
const form = {
    layout: "horizontal",
    rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' },]
    },
    items: [
        { label: '角色名称', field: 'name' },
        { label: $L("DisplayName:IsDefault"), field: 'isDefault', type: "switch" },
        { label: $L("DisplayName:IsPublic"), field: 'isPublic', type: "switch" }
    ]
};

const $createModal = $V.FormModal({
    form,
    title: $L('Create'),
    ok: (values) => {
        return $service.role.create(values)
            .then(() => {
                $message.success($L('Create') + $L('Success'));
                $rolesTable.reload();
            });
    }
}, "#createModal");

const $editModal = $V.FormModal({
    form,
    title: $L('Edit'),
    ok: (values, { id }) => {
        return $service.role.update(id, values)
            .then(() => {
                $message.success($L('Edit') + $L('Success'));
                $rolesTable.reload();
            });
    }
}, "#editModal");


const $rolesTable = $V.DataTable(({ toRef }) => {
    const OnRowEdit = (record) => {
        $editModal.show(record);
    };
    const OnRowPermission = (record) => {
        $permissionEditModal.show("R", record.name);
    };
    const OnRowDelete = (record) => {
        const dataSource = toRef($rolesTable, 'dataSource');
        return $on.Delete($service.role, record, dataSource);
    };

    const OnCreate = () => {
        $createModal.visible = true;
    };

    let auth = $auth.system.role;
    return {
        hasSelection: false,
        service: $service.role,
        querys: [
            { label: '角色名称', field: 'name', type: "input", value: "" },
        ],
        columns: [
            { title: '序号', field: 'index', width: 50, align: 'center', template: "index" },
            { title: '角色名称', field: 'name', template: "name" },
            //{ title: '是否启用', field: 'isActive', align: 'center', width: 80, template: "isActive" },
            //{ title: '默认角色', field: 'isDefault', align: 'center', width: 80, template: "isDefault" },
            {
                title: $L('Actions'), align: 'center', fixed: "right", template: "operation",
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
}, "#rolesTable");