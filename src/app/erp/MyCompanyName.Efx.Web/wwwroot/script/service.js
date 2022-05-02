const $service = {
    company: myCompanyName.identity.company,
    role: myCompanyName.identity.role,
    user: myCompanyName.identity.user,
    profile: myCompanyName.identity.profile,
    permission: myCompanyName.erp.systemService.permission,
    tools: myCompanyName.erp.systemService.tools,
    resellerLevel: myCompanyName.erp.resellerService.resellerLevel,
    bankCard: myCompanyName.erp.financesService.bankCard,
}
const $upload = {
    SavePictureUrl: "/file/save-picture",
    SaveAvatarUrl: "/file/save-avatar"
}
const $on = {
    UpdateActive: (service, record, checked) => {
        const hide = $message.loading($L('ProcessingWithThreeDot'), record.id);
        return service.updateActive(record.id, checked)
            .then(() => {
                $message.success(`${checked ? "启用" : "禁用"}成功！`, record.id);
                record.isActive = checked;
            }).catch(hide);
    },
    Delete: (service, record, dataSource) => {
        const hide = $message.loading($L('ProcessingWithThreeDot'), record.id);
        return service.delete(record.id)
            .then(() => {
                $message.success(`删除成功！`, record.id);
                if (Vue.isRef(dataSource) && Array.isArray(dataSource.value)) {
                    dataSource.value = dataSource.value.filter(f => f.id != record.id);
                }
            }).catch(hide);
    },
    SendSmsCaptcha: (mobile, smsNo) => {
        const timestamp = new Date().getTime();
        const key = "rKNoSQdaHdrI2gVW";
        const sign = MD5(`${mobile}${timestamp}${key}`);
        const hide = $message.loading($L('ProcessingWithThreeDot'), timestamp);
        return $service.tools.sendSmsCaptcha({ mobile, smsNo, timestamp, sign })
            .then(() => {
                $message.success(`发送成功！`, timestamp);
            }).catch(hide);
    }
}
