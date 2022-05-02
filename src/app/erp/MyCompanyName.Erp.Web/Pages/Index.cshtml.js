$V.Setup(({ ref }) => {
    const activeKey = ref();
    const panes = ref([]);

    const open = (pane) => {
        $('.a-left-menu-panel-main .close').click();
        const exist = panes.value.some(p => p.key == pane.key);
        if (!exist && pane) {
            panes.value.push(pane);
            pane = panes.value.find(p => p.key == pane.key);
            pane.render = true;
            pane.contextmenu = [
                { title: $L("关闭其他"), OnClick: () => removeOther(pane.key) },
                { title: $L("关闭全部"), OnClick: removeAll },
                { type: "divider" },
                { title: $L("Refresh"), OnClick: () => refresh(pane) }
            ]
        }
        pane.loading = true;
        activeKey.value = pane.key;
    };
    const refresh = pane => {
        const targetKey = pane.key;
        if (activeKey.value !== targetKey) { return; }
        pane.render = false; 
        setTimeout(() => {
            pane.loading = true;
            pane.render = true;
        }, 10);
    };
    const removeAll = () => {
        panes.value = panes.value || [];
        panes.value = panes.value.filter(pane => pane.closable === false);
        activeKey.value = panes.value[0].key;
    };
    const removeOther = targetKey => {
        panes.value = panes.value || [];
        panes.value = panes.value.filter(pane => pane.key === targetKey || pane.closable === false);
        if (panes.value.indexOf(activeKey.value) == -1) { activeKey.value = targetKey; }

    };
    const remove = targetKey => {
        let lastIndex = 0;
        panes.value = panes.value || [];
        panes.value.forEach((pane, i) => {
            if (pane.key === targetKey) {
                lastIndex = i - 1;
            }
        });
        panes.value = panes.value.filter(pane => pane.key !== targetKey);
        if (panes.value.length && activeKey.value === targetKey) {
            if (lastIndex >= 0) {
                activeKey.value = panes.value[lastIndex].key;
            } else {
                activeKey.value = panes.value[0].key;
            }
        }
    };

    const OnOpen = (event) => {
        const _nemu = event.target;
        let pane = {
            key: `${_nemu.dataset.key}`,
            src: _nemu.dataset.url,
            title: _nemu.title,
        }
        open(pane);
    };
    const OnEdit = targetKey => { remove(targetKey); };

    open({
        key: 'Menu.Home',
        title: $L("Menu.Home"),
        src: '/Home',
        icon: "fa fa-home",
        closable: false
    });
    return {
        panes,
        activeKey,
        OnEdit,
        OnOpen,
    };
}, "#adminLayout")