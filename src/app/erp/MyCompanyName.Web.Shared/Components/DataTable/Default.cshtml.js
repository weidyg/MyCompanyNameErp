
$V.DataTable = (obj, selectors) => {
    if (typeof obj == "function") { obj = obj(Vue); }
    let { hasSelection, rowKey, service, querys, actions, columns, methods: events } = obj;
    rowKey = rowKey || "id";
    events = events || {};
    columns = columns || [];
    querys = querys || [];
    actions = actions || [];
    let i = 0;
    let queryForm = {};
    querys.forEach(f => { queryForm[f.field] = f.value; });

    var $vm = $V.Setup(({ ref, reactive, toRefs, onMounted, nextTick, computed, watch }) => {
        queryForm = reactive(queryForm);
        columns = reactive(columns.filter(f => f.visible !== false));

        const formRef = ref(null);
        const tableRef = ref(null);
        const search = reactive({
            loading: false,
            collapsed: false,
            togele: () => { search.collapsed = !search.collapsed }
        });
        const columnSet = reactive({
            visible: false,
            checked: computed(() => columns.every(s => s && s.visible)),
            indeterminate: computed(() => !columnSet.checked && columns.some(s => s && s.visible)),
            click: () => {
                const _checked = columnSet.checked;
                columns.forEach(f => f.visible = !_checked);
            },
            reset: () => {
                columns.sort((a, b) => a.sort - b.sort);
                columns.forEach(f => f.visible = true);
            },
            ondragstart: (e, index, column) => {
                e.dataTransfer.setData('data', JSON.stringify({ index, column }));
                e.target.style.opacity = 0.5;
            },
            ondragenter: (e) => { },
            ondrag: (e) => { },
            ondragover: (e) => {
                e.preventDefault();
            },
            ondragleave: (e) => { },
            ondrop: (e, index) => {
                let data = JSON.parse(e.dataTransfer.getData("data"));
                columns.splice(data.index, 1);
                columns.splice(index, 0, data.column);
            },
            ondragend: (e) => {
                e.target.style.opacity = 1;
            },
        });
        const pagination = reactive({
            current: 1,
            pageSize: 20,
            total: 0,
            showSizeChanger: true,
            showQuickJumper: true,
            pageSizeOptions: ['20', '50', '100', '150', '200'],
            showTotal: (total, range) => $L("PagerInfo{0}{1}{2}", range[0], range[1], total)
        });
        const scroll = reactive({
            y: true,
            x: 600,
            scrollToFirstRowOnChange: true,
        });
        const state = reactive({
            loading: false,
            dataSource: [],
        });
        const tableLocale = reactive({ emptyText: undefined });
        let _queryParam = reactive({
            current: pagination.current,
            pageSize: pagination.pageSize,
            query: queryForm,
            filters: {},
            sorter: {},
        });
        const reload = (obj) => {
            obj = obj || {};
            state.loading = true;
            let { current, pageSize, query } = _queryParam;
            //console.log('reload', current, pageSize, query);
            return service
                .getPageList(current, pageSize, query || {})
                .then((result) => {
                    //console.log(result);
                    let tempIndex = 1;
                    pagination.current = current;
                    pagination.pageSize = pageSize;
                    pagination.total = result.totalCount;
                    state.dataSource = result.items || [];
                    state.dataSource.forEach(f => { f.index = (current - 1) * pageSize + (tempIndex++); });
                    state.loading = false;
                    if (obj.tips) {
                        const tips = typeof obj.tips == "string" ? obj.tips : $L('Refresh') + $L('Success');
                        $message.success(tips);
                    }
                    tableLocale.emptyText = undefined;
                }).catch(({ status }) => {
                    state.loading = false;
                    status = [401, 403, 404].indexOf(status) == -1 ? "" : status;
                    tableLocale.emptyText
                        = $L(`DefaultErrorMessage${status}`)
                        + $L(`DefaultErrorMessage${status}Detail`);
                });
        };
        const onChange = (_pagination, filters, sorter) => {
            //console.log('params', _pagination, filters, sorter);
            let { current, pageSize } = _pagination;
            _queryParam.current = current;
            _queryParam.pageSize = pageSize;
            _queryParam.filters = filters;
            _queryParam.sorter = sorter;
            reload();
        };
        const onSearch = () => {
            //console.log('onSearch', queryForm);
            _queryParam.query = queryForm;
            search.loading = true;
            reload().then(() => {
                search.loading = false;
            }).catch(() => {
                search.loading = false;
            });
        };
        const rowSelection = hasSelection === false ? undefined : reactive({
            selectedRowKeys: [],
            onChange: (selectedRowKeys) => {
                rowSelection.selectedRowKeys = selectedRowKeys||[];
            }
        });
        const selectedRowKeys = computed(() => rowSelection.selectedRowKeys);
        //if (typeof events == "function") { events = events(state, { reload }); }
        //if (typeof actions == "function") { actions = actions(state, { reload }); }
        actions = actions?.filter(f => f.visible !== false) || [];
        let InitActions = () => {
            actions.forEach(f => {
                InitActionsClick(f);
                if (f.items && f.items.length > 0) {
                    f.items = f.items.filter(f => f.visible !== false);
                    if (f.items.length > 0) {
                        f.isGroup = true;
                        f.items.forEach(fe => {
                            InitActionsClick(fe);
                        })
                    } else {
                        f.visible = false;
                    }
                }
            })
        }
        let InitActionsClick = (f) => {
            if (typeof f.onClick == "function") {
                let onClick = f.onClick;
                f.onClick = () => { onClick(reactive(f)) };
            }
        }

        columns.forEach(f => {
            f.sort = i++;
            f.visible = true;
            f.dataIndex = f.dataIndex || f.field;
            f.template = (!f.template && f.actions && f.actions.length > 0) ? "operation" : f.template;
            f.slots = (f.template ? Object.assign((f.slots || {}), { customRender: f.template.toLowerCase() }) : f.slots);
            if (f.actions) {
                if (f.actions.every(e => e.visible == false)) {
                    f.visible = false
                } else {
                    f.align = 'center';
                    f.actions = f.actions || [];
                    f.actions.forEach(a => {
                        let onClick = a.onClick;
                        a.onClick = (record) => {
                            a[`loading${record.index}`] = true;
                            let result = onClick(record);
                            if (isPromise(result) && !a.confirm) {
                                result.then((r) => {
                                    a[`loading${record.index}`] = false;
                                }).catch((e) => {
                                    a[`loading${record.index}`] = false;
                                });
                            } else {
                                a[`loading${record.index}`] = false;
                            }
                            return result;
                        }
                    });
                }
            };
            if (f.template == "isActive") {
                events.OnUpdateIsDefault = (record, ev) => {
                    const checked = ev.target.checked;
                    const hide = $message.loading($L('ProcessingWithThreeDot'), record.id);
                    return service
                        .updateIsDefault(record.id, checked)
                        .then(() => {
                            $message.success(`操作成功！`, record.id);
                            record.isDefault = checked;
                            if (checked) {
                                $rolesTable.dataSource
                                    .filter(f => f.isDefault && f.id != record.id)
                                    .forEach(fe => fe.isDefault = false);
                            }
                        }).catch(hide);
                };
            };
            if (f.template == "isDefault") {
                events.OnUpdateActive = (record, checked) => {
                    return $on.UpdateActive(service, record, checked);
                };
            };
        });

        onMounted(() => {
            reload();
            InitActions();
            nextTick(function () {
                if (tableRef.value || formRef.value) {
                    windowResizeEvent(() => {
                        if (tableRef.value) {
                            const tableDom = tableRef.value.$el;
                            const tableBody = $(tableDom).find(".ant-table-body");
                            const tablePagination = $(tableDom).find(".ant-table-pagination");
                            const windowHeight = $(window).height();
                            const tableBodyTop = tableBody.offset().top;
                            const tablePageHeight = tablePagination.outerHeight(true) | 48.5;
                            scroll.y = windowHeight - tableBodyTop - tablePageHeight - 1;
                        }
                        if (formRef.value) {
                            const formDom = formRef.value.$el;
                            const windowHeight = $(window).height();
                            const formTop = $(formDom).offset().top;
                            const formHeight = windowHeight - formTop - 48.5 - 1;
                            formDom.style.overflowY = "scroll"
                            formDom.style.maxHeight = `${formHeight}px`;
                        }
                    });
                    $(window).trigger("resize");
                }
            });
        });
        const until = reactive({
            formatDateTime: (text) => {
                let tempMoment = moment(text);
                if (tempMoment._isValid) {
                    return tempMoment.format("yyy-MM-DD HH:mm:ss");
                }
                return "";
            }
        });
        //const allTreeData = [
        //    {
        //        title: 'parent 1',
        //        key: '0-0',
        //        children: [
        //            {
        //                title: 'parent 1-0',
        //                key: '0-0-0',
        //                disabled: true,
        //                children: [
        //                    { title: 'leaf', key: '0-0-0-0', disableCheckbox: true },
        //                    { title: 'leaf', key: '0-0-0-1' },
        //                ],
        //            },
        //            {
        //                title: 'parent 1-1',
        //                key: '0-0-1',
        //                children: [{ key: '0-0-1-0', slots: { title: 'title0010' } }],
        //            },
        //        ],
        //    },
        //];
        //const leftTree = reactive({
        //    searchValue: "",
        //    expandedKeys: [],
        //    selectedKeys: [],
        //    autoExpandParent: true,
        //    treeData: allTreeData,
        //    onExpand: keys => {
        //        leftTree.expandedKeys = keys;
        //        leftTree.autoExpandParent = false;
        //    },
        //    onExpanded: isAll => {

        //    },
        //    onSearch: value => {

        //    }
        //});
        //const getParentKey = (key, tree) => {
        //    console.log("key, tree", key, tree);
        //    let parentKey;
        //    for (let i = 0; i < tree.length; i++) {
        //        const node = tree[i];
        //        if (node.children) {
        //            if (node.children.some(item => item.key === key)) {
        //                parentKey = node.key;
        //            } else if (getParentKey(key, node.children)) {
        //                parentKey = getParentKey(key, node.children);
        //            }
        //        }
        //    }
        //    return parentKey;
        //};
        //const dataList = [];
        //const generateList = data => {
        //    for (let i = 0; i < data.length; i++) {
        //        const node = data[i];
        //        const key = node.key;
        //        const title = node.title;
        //        dataList.push({ key, title });
        //        if (node.children) {
        //            generateList(node.children);
        //        }
        //    }
        //};
        //generateList(allTreeData);
        //watch(() => leftTree.searchValue, value => {
        //    console.log("value", value);
        //    const expanded = dataList.map(item => {
        //        console.log("item.title, value", item.title, value);
        //        console.log("item.title?.indexOf", item.title?.indexOf(value) > -1);
        //        if (item.title?.indexOf(value) > -1) {
        //            return getParentKey(item.key, allTreeData);
        //        } return null;
        //    }).filter((item, i, self) => item && self.indexOf(item) === i);
        //    console.log("expanded", expanded);
        //    leftTree.expandedKeys = expanded;
        //    leftTree.searchValue = value;
        //    leftTree.autoExpandParent = true;
        //    console.log("leftTree", leftTree);
        //});
        return Object.assign({
            formRef,
            tableRef,
            rowKey,
            scroll,
            pagination,
            querys,
            actions,
            columns,
            queryForm,
            onChange,
            onSearch,
            reload,
            search,
            columnSet,
            tableLocale,
            rowSelection,
            selectedRowKeys,
            //leftTree,
        }, events,
            toRefs(until),
            toRefs(state));
    }, selectors);
    return $vm;
};