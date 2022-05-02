$V.FormModal = (obj, selectors) => {
    if (isFunction(obj)) { obj = obj(Vue); }
    let { form, formLayout, title, ok, mask, maskClosable, closable } = obj;
    maskClosable = maskClosable || false;
    form = form || {};
    let formModel = {};
    let formItems = [];
    let formRules = form.rules || {};
    let isFormTabItems = false;
    let tempAllItems = form.items || [];
    let tempTabs = [];

    var $vm = $V.Setup(({ ref, reactive, computed, }) => {
        /* ----------------------------------------------*/
        let forItem = (f) => {
            if (f.field) {
                formModel[f.field] = f.value;
            }
            if (f.type == "upload-picture") {
                formRules[f.field] = formRules[f.field] || [];
                formRules[f.field].push({
                    trigger: 'blur',
                    validator: (rule, value) => {
                        return new Promise((resolve, reject) => {
                            if (uploadError[rule.field]) {
                                reject(uploadError[rule.field]);
                            } else {
                                resolve();
                            }
                        });
                    },
                });
            }
            if (f.type == "checkboxs" && typeof f.options == "function") {
                f.options = f.options();
                if (typeof f.options.then == "function") {
                    f.options.then(result => {
                        f.options = result || [];
                    });
                }
            }

            return f;
        };
        tempAllItems.forEach(f => {
            f = forItem(f);
            if (f.items && Array.isArray(f.items)) {
                f.items.forEach(f => { forItem(f); });
                tempTabs.push(f);
            } else {
                formItems.push(f);
            }
        });
        if (tempTabs.length > 0) {
            isFormTabItems = true;
            if (formItems.length > 0) {
                tempTabs.splice(0, 0, { title: '基本信息', items: formItems });
            }
            formItems = tempTabs;
        }
        /* ----------------------------------------------*/
        const formRef = ref(null);
        formModel = reactive(formModel);
        formRules = reactive(formRules);
        formItems = ref(formItems);
        formLayout = form.layout || 'horizontal';
        const formItemLayout = computed(() => {
            return formLayout === 'horizontal'
                ? {
                    labelCol: { span: 5 },
                    wrapperCol: { span: 19 },
                }
                : {};
        });

        const visible = ref(false);
        const okButtonProps = reactive({ loading: false });
        const handleCancel = (e) => {
            formRef.value.resetFields();
        };
        const handleOk = (e) => {
            formRef.value.validate()
                .then((values) => {
                    okButtonProps.loading = true;
                    let okSubmit = isFunction(ok) ? ok(values, formModel) : ok;
                    if (isPromise(okSubmit)) {
                        okSubmit.then((result) => {
                            okButtonProps.loading = false;
                            visible.value = false;
                            formRef.value.resetFields();
                        }).catch((error) => {
                            okButtonProps.loading = false;
                            //console.log('ok catch', error);
                        })
                    }
                })
                .catch((error) => {
                    console.log('formRef validate', error);
                });
        };
        /* ------------------------------Upload-------------------------------------------*/
        const uploadUrl = $upload.SavePictureUrl;
        const uploadError = reactive({});
        tempAllItems.forEach(f => {
            if (f.type == "upload-picture") {
                formRules[f.field] = formRules[f.field] || [];
                formRules[f.field].push({
                    trigger: 'blur',
                    validator: (rule, value) => {
                        return new Promise((resolve, reject) => {
                            if (uploadError[rule.field]) {
                                reject(uploadError[rule.field]);
                            } else {
                                resolve();
                            }
                        });
                    },
                });
            }
        });
        const onUploadChange = (file, item) => {
            if (file.status == "uploading") {
                uploadError[item.field] = $L('UploadingWithThreeDot');
            } else if (file.status == "done") {
                uploadError[item.field] = null;
            } else if (file.status == "error") {
                uploadError[item.field] = file?.response?.error?.message;
            } else if (file.status == "removed") {
                uploadError[item.field] = null;
            }
        };
        /* --------------------------------Email-----------------------------------------*/
        const emailOptions = ref([]);
        const onEmailSearch = val => { emailOptions.value = appendEmailSuffix(val); };
        /* -------------------------------------------------------------------------*/
        const show = (formValues) => {
            visible.value = true;
            if (formValues) {
                Object.keys(formValues).forEach(key => {
                    let value = formValues[key];
                    if (value !== undefined) { formModel[key] = value; }
                })
            }
            return Promise.resolve();
        };
        return {
            uploadUrl,
            onUploadChange,
            emailOptions,
            onEmailSearch,
            title,
            visible,
            handleOk,
            handleCancel,
            okButtonProps,
            mask,
            maskClosable,
            closable,
            formRef,
            formItems,
            formModel,
            formRules,
            formLayout,
            formItemLayout,
            isFormTabItems,
            show
        };
    }, selectors);
    return $vm;
};