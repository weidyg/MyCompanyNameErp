const $resellerTable = $V.DataTable(({ toRef }) => {
    return {
        service: $service.resellerInfo,
        querys: [
            { label: '公司名称', field: 'name', type: "input", value: "" },
        ],
        columns: [
            { title: '公司名称', field: 'name', width: 220, },
            { title: '', field: '' },
        ],
        actions: [
           
        ]
    }
}, "#resellerTable");