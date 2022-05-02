let validatePass = (formRef, formModel, value) => {
    return $validate.password(value);
};

let validatePass2 = (formModel, value) => {
    return $validate.checkPass(value, formModel.password);
};

let acaSmsNo = "apply_company_account";
$V.Setup(({ ref, reactive }) => {
    const formRef = ref(null);
    const applyForm = reactive({
        /*  companyName: "商通",
          userName: "weidyg",
          password: "1q2w3E*",
          checkPass: "1q2w3E*",
          surname: "张",
          name: "三",
          phoneNumber: "18698376676",
          smsCaptcha: "",
          email: "weidyg@163.com",*/
    });
    const rules = {
        companyName: [{ required: true, message: '请输入公司名称', trigger: 'blur' },],
        userName: [{ required: true, message: '请输入用户名', trigger: 'blur' },],
        password: [{ required: true, validator: (rule, value) => validatePass(formRef, applyForm, value), trigger: 'change', },],
        checkPass: [{ required: true, validator: (rule, value) => validatePass2(applyForm, value), trigger: 'change', },],
        phoneNumber: [{ required: true, pattern: $pattern.phoneNumber, message: '请输入正确手机号码', trigger: 'change' },],
        smsCaptcha: [{ required: true, message: '请输入验证码', trigger: 'change' },],
        email: [{ type: 'email', message: '请输入正确邮箱', trigger: 'change', },],
    };
    const loading = ref(false);
    const handleFinish = values => {
        loading.value = true;
        values.smsNo = acaSmsNo;
        values.checkPass = undefined;
        $service.company.apply(values)
            .then(() => {
                loading.value = false;
            }).catch(() => {
                loading.value = false;
            });
    };
    const handleFailed = errors => {
        console.log(errors);
    };
    const smsBtnText = "获取";
    const smsBtn = reactive({
        disabled: false,
        text: smsBtnText,
        send: () => {
            formRef.value.validate(['phoneNumber']).then((values) => {
                $on.SendSmsCaptcha(values.phoneNumber, acaSmsNo)
                    .then(() => {
                        let s = 120;
                        smsBtn.disabled = true;
                        smsBtn.text = `${s}s`
                        const smsBtnTimer = setInterval(() => {
                            smsBtn.text = `${--s}s`
                            if (s == 0) {
                                clearInterval(smsBtnTimer);
                                smsBtn.disabled = false;
                                smsBtn.text = smsBtnText;
                            }
                        }, 1000);
                    });
            });
        }
    });
    /* --------------------------------Email-----------------------------------------*/
    const emailOptions = ref([]);
    const onEmailSearch = val => { emailOptions.value = appendEmailSuffix(val); };
    /* -------------------------------------------------------------------------*/
    return {
        smsBtn,
        rules,
        formRef,
        applyForm,
        handleFinish,
        handleFailed,
        loading,
        emailOptions,
        onEmailSearch
    };
}, "#account-apply");

