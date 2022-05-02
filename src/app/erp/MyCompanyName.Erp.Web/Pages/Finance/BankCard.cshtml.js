let sciTimer = true;
const setCardInfo = (e, isEidt) => {
    clearTimeout(sciTimer);
    sciTimer = setTimeout(() => {
        const accountNo = e.target.value;
        if (accountNo) {
            getBankCardInfo(accountNo).then((info) => {
                const vm = (isEidt ? $bCEModal : $bCCModal);
                if (info.bankName) { vm.formModel.bankName = info.bankName; }
                if (info.cardType) { vm.formModel.cardType = info.cardType; }
            });
        }
    }, 800);
};
const bCForm = (isEidt) => {
    return {
        rules: {
            accountNo: [{ required: true, message: '请输入卡号/账号', whitespace: true }],
            bankName: [{ required: true, message: '请输入银行/支付机构名称' }],
            realName: [{ required: true, message: '请输入真实姓名' },],
        },
        items: [
            { label: '卡号/账号', field: 'accountNo', change: (e) => setCardInfo(e, isEidt) },
            { label: '银行名称', field: 'bankName' },
            { label: '卡类型', field: 'cardType' },
            { label: '真实姓名', field: 'realName' },
            {
                label: '二维码', field: 'qrCode', type: "upload-picture",
                data: () => {
                    const vm = (isEidt ? $bCEModal : $bCCModal);
                    return { type: "qrcode", name: vm.formModel.accountNo };
                },
                //beforeUpload: (file) => {
                //    const vm = (isEidt ? $bCEModal : $bCCModal);
                //    if (!vm.formModel.accountNo) {
                //        $message.error('请先输入卡号/账号');
                //        return false;
                //    }
                //    return true;
                //}
            },
        ]
    }
};
const $bCCModal = $V.FormModal({
    title: $L('Create'),
    form: bCForm(),
    ok: (values) => {
        return $service.bankCard.create(values)
            .then(() => {
                $message.success($L('Create') + $L('Success'));
                $bankCardList.reload();
            });
    }
}, "#bCCModal");
const $bCEModal = $V.FormModal({
    title: $L('Edit'),
    form: bCForm(true),
    ok: (values, { id }) => {
        return $service.bankCard.update(id, values)
            .then(() => {
                $message.success($L('Edit') + $L('Success'));
                $bankCardList.reload();
            });
    }
}, "#bCEModal");
const $bankCardList = $V.Setup(({ reactive, toRef, toRefs, onMounted }) => {
    const query = reactive({});
    const state = reactive({
        loading: false,
        dataSource: [],
    });
    const auth = reactive($auth.finance.bankCard);
    const reload = (obj) => {
        obj = obj || {};
        state.loading = true;
        let { current, pageSize } = pagination;
        $service.bankCard.getPageList(current, pageSize, query)
            .then((result) => {
                state.dataSource = result.items || [];
                state.dataSource.forEach(f => {
                    //f.qrCode = f.qrCode || '//';
                    f.creationTime = moment(f.creationTime).format("yyyy-MM-DD");
                })
                pagination.total = result.totalCount;
                if (obj.tips) {
                    const tips = typeof obj.tips == "string" ? obj.tips : $L('Refresh') + $L('Success');
                    $message.success(tips);
                }
                state.loading = false;
            }).catch(({ status }) => {
                state.loading = false;
            });
    }
    const pagination = reactive({
        current: 1,
        pageSize: 20,
        total: 0,
        onChange: page => {
            current = page;
            reload();
        },
    });
    const onShowCreate = () => { $bCCModal.show(); };
    const onShowUpdate = (record) => { $bCEModal.show(record); };
    const onUpdateActive = (checked, record) => {
        $on.UpdateActive($service.bankCard, record, checked);
    };
    const onDelete = (record) => {
        const dataSource = toRef(state, 'dataSource');
        return $on.Delete($service.bankCard, record, dataSource);
    };
    onMounted(() => {
        reload();
    });
    return Object.assign({
        auth,
        query,
        pagination,
        onShowCreate,
        onShowUpdate,
        onUpdateActive,
        onDelete,
        reload,
    }, toRefs(state));
}, "#bankCard");