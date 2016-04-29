$(function () {
    $("#grid").jqGrid({
        url: "/BillingJasa/Anamnesis",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Bagian Gigi', 'Tindakan', 'Harga'],
        colModel: [         
            {
                key: false, name: 'Gigi', index: 'Gigi', editable: true, edittype: 'select',
                editoptions:
                    {
                        value:
                            {
                                'k11': 'Kuadran 1 : 1',
                                'k12': 'Kuadran 1 : 2',
                                'k13': 'Kuadran 1 : 3',
                                'k14': 'Kuadran 1 : 4',
                                'k15': 'Kuadran 1 : 5',
                                'k16': 'Kuadran 1 : 6',
                                'k17': 'Kuadran 1 : 7',
                                'k18': 'Kuadran 1 : 8'
                            }
                    }
            },
            { key: false, name: 'NamaTindakan', index: 'NamaTindakan', editable: true },
            { key: false, name: 'Harga', index: 'Harga', editable: true, type: Float32Array }],
        pager: jQuery("#pager"),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Tindakan kepada Pasien',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/BillingJasa/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/BillingJasa/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/BillingJasa/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});