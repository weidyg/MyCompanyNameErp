
const form = {
    layout: "horizontal",
    rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' },],
        discount: [{ required: true, type: 'number', message: '请输入折扣', trigger: 'blur' }]
    },
    items: [
        { label: '等级名称', field: 'name' },
        { label: '等级折扣', field: 'discount', type: "number", value: 1, min: 0.01, max: 1.00, step: 0.1, precision: 2 },
        { label: '是否启用', field: 'isActive', type: "switch", value: true },
        { label: '等级描述', field: 'description', type: "textarea", autoSize: { minRows: 2, maxRows: 5 } },
    ]
};

const $createModal = $V.FormModal({
    form,
    title: $L('Create'),
    ok: (values) => {
        return $service.resellerLevel.create(values)
            .then(() => {
                $message.success($L('Create') + $L('Success'));
                $levelTable.reload();
            });
    }
}, "#createModal");

const $editModal = $V.FormModal({
    form,
    title: $L('Edit'),
    ok: (values, { id }) => {
        return $service.resellerLevel.update(id, values)
            .then(() => {
                $message.success($L('Edit') + $L('Success'));
                $levelTable.reload();
            });
    }
}, "#editModal");


const $levelTable = $V.DataTable(({ toRef }) => {
    const OnRowEdit = (record) => {
        $editModal.show(record);
    };
    const OnRowDelete = (record) => {
        const dataSource = toRef($levelTable, 'dataSource');
        return $on.Delete($service.resellerLevel, record, dataSource);
    };
    const OnCreate = () => {
        $createModal.visible = true;
    };
    const OnEdit = () => {
        let selectedRowKeys = $levelTable.selectedRowKeys;
        const selectedLength = selectedRowKeys.length;
        if (selectedLength !== 1) {
            $message.warn(`请选择且只选择一个要编辑的记录！`);
        } else {
            const id = selectedRowKeys[0];
            const initValue = $levelTable.dataSource.find(f => f.id == id);
            $editModal.show(initValue);
        }
    };
    const OnDelete = () => {
        let selectedRowKeys = $levelTable.selectedRowKeys;
        const selectedLength = selectedRowKeys.length;
        if (selectedLength < 1) {
            $message.warn(`请至少选择一个要删除的记录！`);
        } else {
            $confirm({
                title: "提示",
                icon: "exclamation",
                content: `选中的 ${selectedLength} 条记录将被删除，你确定吗?`,
                onOk: () => {
                    const key = "_resellerLevelAppServicedeleteMany";
                    $message.loading($L('ProcessingWithThreeDot'), key);
                    return $service.resellerLevel
                        .deleteMany(selectedRowKeys)
                        .then(() => {
                            $message.success(`删除成功！`, key);
                            $levelTable.reload();
                        });
                },
            });
        }
    };
    let auth = $auth.reseller.level;
    return {
        service: $service.resellerLevel,
        querys: [
            { label: '等级名称', field: 'name', type: "input", value: "" },
        ],
        columns: [
            { title: '等级名称', field: 'name', width: 150, },
            { title: '等级折扣', field: 'discount', align: 'center', width: 80, template: "discount" },
            { title: '是否启用', field: 'isActive', align: 'center', width: 80, template: "isActive" },
            { title: '默认等级', field: 'isDefault', align: 'center', width: 80, template: "isDefault" },
            { title: '等级描述', field: 'description', ellipsis: true, },
            {
                title: $L('Actions'), align: 'center', fixed: "right", width:120, template: "operation",
                actions: [
                    { title: $L('Edit'), onClick: OnRowEdit, visible: auth.update },
                    { title: $L('Delete'), onClick: OnRowDelete, visible: auth.delete, confirm: `${$L("ItemWillBeDeletedMessage")}${$L("AreYouSure")}`, }
                ]
            },
        ],
        actions: [
            { text: $L('Create'), icon: "fa fa-plus", type: "primary", onClick: OnCreate, visible: auth.create },
            { text: $L('Edit'), icon: "fa fa-edit", onClick: OnEdit, visible: auth.update },
            { text: $L('Delete'), icon: "fa fa-trash", type: "danger", onClick: OnDelete, visible: auth.delete },
            //{ text: $L('Refresh'), icon: "fa fa-refresh", type: "dashed", onClick: onRefresh },
            {
                text: "组合按钮(测试)",
                items: [
                    { text: $L('Create'), icon: "fa fa-plus", type: "primary", onClick: OnCreate, visible: auth.create },
                    { text: $L('Edit'), icon: "fa fa-edit", onClick: OnEdit, visible: auth.update },
                    { text: $L('Delete'), icon: "fa fa-trash", type: "danger", onClick: OnDelete, visible: auth.delete, },
                ]
            },
        ],
        methods: {
            //OnUpdateIsDefault,
            //OnUpdateActive
        }
    }
}, "#levelTable");