let validatePassword = (isEidt, value) => {
    const vm = (isEidt ? $editModal : $createModal);
    return $validate.password(value);
};

let validatePass2 = (isEidt, value) => {
    const vm = (isEidt ? $editModal : $createModal);
    return $validate.checkPass(value, vm.formModel.password);
};

let AssignableRoles = [];
const ceForm = (isEidt) => {
    return {
        layout: "horizontal",
        rules: {
            userName: [{ required: true, message: '请输入用户名', trigger: 'blur' },],
            password: [{ required: true, validator: (rule, value) => validatePassword(isEidt, value), trigger: 'change', },],
            checkPass: [{ required: true, validator: (rule, value) => validatePass2(isEidt, value), trigger: 'change', },],
            email: [{ required: true, type: 'email', message: '请输入正确邮箱', trigger: 'change', },],
            phoneNumber: [{ required: true, pattern: $pattern.phoneNumber, message: '请输入正确手机号码', trigger: 'change' },]
        },
        items: [
            {
                title: '用户信息', items: [
                    { label: '用 户 名', field: 'userName', disabled: isEidt && !UserSet.IsUserNameUpdateEnabled },
                    { label: '姓', field: 'surname' },
                    { label: '名', field: 'name' },
                    { label: '密码', field: 'password', type: "password", visible: !isEidt },
                    { label: '确认密码', field: 'checkPass', type: "password", visible: !isEidt },
                    { label: '手机号码', field: 'phoneNumber' },
                    { label: '邮箱地址', field: 'email', type: "email", disabled: isEidt && !UserSet.IsEmailUpdateEnabled },
                    {
                        label: '锁定启用', field: 'lockoutEnabled', type: "switch",
                        tip: '账号密码连续输错{0}次，将锁定账号{1}分钟'.format(LockoutSet.MaxFailedAccessAttempts, (LockoutSet.LockoutDuration / 60))
                    }
                ]
            },
            {
                title: '角色', items: [
                    {
                        label: '', field: 'roleNames', type: "checkboxs", options: () => {
                            if (AssignableRoles.length > 0) { return AssignableRoles.map(m => { return { label: m.name, value: m.name } }); }
                            else {
                                return $service.user
                                    .getAssignableRoles()
                                    .then((result) => {
                                        AssignableRoles = result || [];
                                        return AssignableRoles.map(m => { return { label: m.name, value: m.name } });
                                    });
                            }
                        }
                    }
                ]
            },
        ]
    }
};


const $createModal = $V.FormModal({
    form: ceForm(),
    title: $L('Create'),
    ok: (values) => {
        return $service.user.create(values)
            .then(() => {
                $message.success($L('Create') + $L('Success'));
                $userTable.reload();
            });
    }
}, "#createModal");

const $editModal = $V.FormModal({
    form: ceForm(true),
    title: $L('Edit'),
    ok: (values, { id }) => {
        return $service.user.update(id, values)
            .then(() => {
                $message.success($L('Edit') + $L('Success'));
                $userTable.reload();
            });
    }
}, "#editModal");


const $userTable = $V.DataTable(({ toRef }) => {
    const OnRowEdit = (record) => {
        return $service.user
            .getRoles(record.id)
            .then((result) => {
                record.roleNames = result?.map(m => m.name) || [];
                $editModal.show(record);
            });
    };
    const OnRowPermission = (record) => {
        return $permissionEditModal.show("U", record.id);
    };
    const OnRowDelete = (record) => {
        const dataSource = toRef($userTable, 'dataSource');
        return $on.Delete($service.user, record, dataSource);
    };
    const OnCreate = () => {
        return $createModal.show({
            roleNames: AssignableRoles?.filter(f => f.isDefault)?.map(m => m.name) || []
        });
    };
    let auth = $auth.system.user;
    return {
        hasSelection: false,
        service: $service.user,
        querys: [
            { label: $L('KeyWords'), field: 'filter', type: "input", value: "" },
        ],
        columns: [
            { title: '序号', field: 'index', align: 'center', width: 50, template: "index" },
            { title: '用 户 名', field: 'userName', width: 120, },
            { title: '姓 名', field: 'fullname', width: 120, template: "fullname" },
            { title: '邮箱地址', field: 'email', width: 150, },
            { title: '手机号码', field: 'phoneNumber', width: 150, },
            { title: '启用锁定', field: 'lockoutEnabled', width: 80, template: "lockoutEnabled" },
            { title: '锁定解除时间', field: 'lockoutEnd', width: 250, template: "lockoutEnd" },
            //{ title: '是否启用', field: 'isActive', align: 'center', width: 80, template: "isActive" },
            {
                title: $L('Actions'), align: 'center', width: 180, fixed: "right", template: "operation",
                actions: [
                    { title: $L('Edit'), onClick: OnRowEdit, visible: auth.update },
                    { title: $L('Permission'), onClick: OnRowPermission, visible: auth.permissions },
                    { title: $L('Delete'), onClick: OnRowDelete, visible: auth.delete, confirm: `${$L("ItemWillBeDeletedMessage")}${$L("AreYouSure")}`, }
                ]
            },
        ],
        actions: [
            { text: $L('Create'), icon: "fa fa-plus", type: "primary", onClick: OnCreate, visible: auth.create }
        ]
    }
}, "#usersTable");