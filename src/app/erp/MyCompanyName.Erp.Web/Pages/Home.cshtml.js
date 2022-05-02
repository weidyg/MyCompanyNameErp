$V.Setup(({ reactive }) => {
    const appUpdate = reactive({
        title: "系统更新",
        loading: true,
        items: []
    });
    setTimeout(() => {
        for (var i = 0; i < 2; i++) {
            appUpdate.items.push({
                time: "2015-09-01",
                title: "测试测试测试测试"
            });
        }
        appUpdate.loading = false;
    }, 1000)

    return {
        appUpdate
    };
}, "#homePage");