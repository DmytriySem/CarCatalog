$(document).ready(function () {
        $("#brandLink").removeClass("hidden");

        $("#jqg").jqGrid({
            url: autoCompleteUrl,
            contentType: "application/json",
            datatype: "json",
            colNames: ['Car id', 'Color', 'Volume engine', 'Description', 'Last Date', 'Last Price'],
            colModel: [
                { name: 'Id', width: 50, align: 'center', stype: 'text', sortable: true, key: true, search: false },
                {
                    name: 'Color', width: 80, align: 'center', editable: true, edittype: 'select', search: true,
                    editoptions: {
                        dataUrl: '',
                        buildSelect: value = function () {
                            return colorsList;
                        }
                    }
                },
                {
                    name: 'VolumeEngine', width: 100, align: 'center', editable: true, edittype: 'select', search: true,
                    editoptions: {
                        dataUrl: '',
                        buildSelect: value = function () {
                            return volEnginesList;
                        }
                    }
                },
                {
                    name: 'Description', width: 350, editable: true, edittype: 'text', search: false,
                    editoptions: {
                        dataInit: function (elem) {
                            $(elem).width(300);
                        }
                    }
                },
                { name: 'LastDate', index: 'Date', width: 100, align: 'center', search: false },
                { name: 'LastPrice', index: 'Price', width: 80, align: 'center', editable: true, edittype: 'text', search: false }
            ],
            rownumbers: true,
            rownumWidth: 50,
            rowNum: 10,
            height: 230,
            pager: '#jpager',
            sortname: 'Id', // сортировка по умолчанию по столбцу Id
            sortorder: "asc", // порядок сортировки
            viewrecords: true,
            caption: "Cars",
            search: {
                caption: "Search...",
                Find: "Find",
                Reset: "Reset",
                odata: ['equal', 'not equal', 'less', 'less or equal', 'greater', 'greater or equal', 'begins with', 'does not begin with', 'is in', 'is not in', 'ends with', 'does not end with', 'contains', 'does not contain'],
                groupOps: [{ op: "AND", text: "all" }, { op: "OR", text: "any" }],
                matchText: " match",
                rulesText: " rules"
            },
        });

        $("#jqg").jqGrid('navGrid', '#jpager', {
            refresh: true, //?????
            add: true,
            del: true,
            edit: true,
            view: true,
            search: true,
            searchtext: "Search",
            addtext: 'Add',
            deltext: 'Delete',
            edittext: 'Edit',
            viewtext: 'View'
        },
            update("edit"),
            update("add"),
            update("del")
        );
        function update(act) {
            return {
                closeAfterAdd: true, // закрыть после добавления
                height: 250,
                width: 600,
                closeAfterEdit: true, // закрыть после редактирования
                reloadAfterSubmit: true, // обновление
                drag: true,
                onclickSubmit: function (params) {
                    var list = $("#jqg");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    if (act === "add")
                        params.url = createUrl;
                    else if (act === "del")
                        params.url = deleteUrl;
                    else if (act === "edit")
                        params.url = editUrl;
                },
                afterSubmit: function (response, postdata) {
                    // обновление грида
                    $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                    return [true, "", 0]
                }
            };
        };
})