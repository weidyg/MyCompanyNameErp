const $permissionEditModal = $V.Setup(({ ref, reactive, toRefs, computed }) => {
    const data = ref([]);
    const state = reactive({
        entityDisplayName: "",
        visible: false,
        loading: false,
        submitting: false,
        selectAllInAllTabs: false,
        selectPartInAllTabs: false,
    });
    const provider = reactive({ key: "", name: "", });
    const show = (providerKey, providerName) => {
        provider.key = providerKey;
        provider.name = providerName;
        if (!provider.key || !provider.name) { $message.error("Providerkey or providername cannot be empty!"); }
        state.loading = true;
        return $service.permission.get(provider.key, provider.name)
            .then((result) => {
                state.visible = true;
                state.entityDisplayName = result.entityDisplayName;
                data.value = result.groups || [];
                data.value.forEach(group => {
                    group.isGrantedCount = computed(() => group.permissions.filter(p => p.isGranted).length);
                    group.isAllPermissionsGranted = computed({
                        get: () => { return group.permissions.every(p => p.isGranted); },
                        set: val => { group.permissions.forEach(f => { if (!f.inoperable) { f.isGranted = val; } }); }
                    });
                    group.isPartPermissionsGranted = computed(() =>
                        !group.isAllPermissionsGranted
                        && group.permissions.some(p => p.isGranted)
                    );
                    group.inoperable = computed(() => group.permissions.every(p => p.inoperable));
                    group.getSubGroups = () => group.permissions.filter(f => f.depth == 0) || [];
                    group.getSubGroupItems = (subGroup) => group.permissions.filter(f => f.name == subGroup.name || f.rootName == subGroup.name) || [];
                    group.permissions.forEach(p => {
                        if (p.depth == 0) {
                            p.indeterminate = computed(() =>
                                !p.isGranted
                                && group.permissions.some(s => s.rootName == p.name && s.isGranted)
                            );
                        }
                    });
                });
                state.selectAllInAllTabs = computed({
                    get: () => { return data.value.every(p => p.isAllPermissionsGranted); },
                    set: val => {
                        data.value.forEach(f => {
                            if (!f.inoperable) { f.isAllPermissionsGranted = val; }
                        });
                    }
                });
                state.selectPartInAllTabs = computed(() =>
                    !state.selectAllInAllTabs
                    && data.value.some(p => p.isAllPermissionsGranted || p.isPartPermissionsGranted)
                );
                state.loading = false;
            }).catch(() => {
                state.loading = false;
            });
    };

    const handleOk = (e) => {
        state.submiting = true;
        let permissions = [];
        data.value.forEach(group => {
            group.permissions.forEach(p => {
                permissions.push({ name: p.name, isGranted: p.isGranted || p.indeterminate });
            });
        });
        console.log("permissions", permissions);
        $service.permission.update(provider.key, provider.name, { permissions })
            .then(() => {
                state.submitting = false;
                state.visible = false;
                $message.success($L('Edit') + $L('Success'));
            }).catch(() => {
                state.submitting = false;
            });
    };
    const handleCancel = (e) => {
        console.log(e);
        state.visible = false;
    };
    return {
        show,
        data,
        handleOk,
        handleCancel,
        ...toRefs(state),
    };
}, "#permissionEdit");
