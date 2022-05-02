if (top.location !== self.location) { top.location = self.location; }
$V.Setup(({ ref, reactive }) => {
    const layout = { wrapperCol: { span: 24, }, };
    const rules = {
        username: [{ required: true, message: "请输入用户名或邮箱！" }],
        password: [{ required: true, message: "请输入密码！" },],
    };
    const loginForm = reactive({
        username: 'admin',
        password: '1q2w3E*',
    });
    const loading = ref(false);
    const handleFinish = values => {
        loading.value = true;
        abp.ajax({
            url: '/login',
            data: JSON.stringify(values)
        }).done(function (data) {
            loading.value = false;
            abp.notify.success($L("GoingToHomePage"), $L('LoginSuccess'));
            window.location.href = data || "/";
        }).fail(function (jqXHR) {
            loading.value = false;
        });
    };
    const handleFinishFailed = errors => {
        console.log(errors);
    };
    return {
        layout,
        loginForm,
        rules,
        handleFinish,
        handleFinishFailed,
        loading
    };
}, "#login");