$(function () {
    $("#grid").jqGrid({
        url: "/Reservation/GetReservation",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['ReservationId', 'PasienId', 'TglReservasi', 'JadwalId'],
        colModel: [
            { key: true, name: 'ReservationId', index: 'ReservationId', editoptions: { readonly: true, size: 10 } },
            { key: false, name: 'PasienId', index: 'PasienId', editable: true, edittype: 'select' },
            {
                key: false, name: 'TglReservasi', index: 'TglReservasi', editable: true, editoptions: {
                    size: 12,
                    dataInit: function (el) { $(el).datepicker({ dateFormat: 'yy-mm-dd' }); },
                    sorttype: "date"
                }
            },
            { key: false, name: 'JadwalId', index: 'JadwalId', editable: true, edittype: 'select', editoptions: { value: { 'Sen' : 'Senin', 'Sel' : 'Selasa', 'Rab' : 'Rabu', 'Kam' : 'Kamis', 'Jum' : 'Jumat' } } }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Daftar Reservasi',
        emptyrecords: 'No record to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            ReservationId: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid("#pager", { edit: true, add: true, del: true, search: false, refresh: true },
       {
           // edit options
           zIndex: 100,
           url: "/Reservation/Edit",
           closeOnEscape: true,
           closeAfterEdit: true,
           recreateForm: true,
           afterComplete: function (respone) {
               if (respone.responeText) {
                   alert(response.responeText);
               }
           }
       },
       {
           // add options
           zIndex: 100,
           url: "/Reservation/Create",
           closeOnEscape: true,
           closeAfterEdit: true,
           afterComplete: function (respone) {
               if (respone.responeText) {
                   alert(response.responeText);
               }
           }
       },
       {
           // delete options
           zIndex: 100,
           url: "/Reservation/Delete",
           closeOnEscape: true,
           closeAfterEdit: true,
           recreateForm: true,
           msg: "Are you sure you want to delete this reservation ?",
           afterComplete: function (respone) {
               if (respone.responeText) {
                   alert(response.responeText);
               }
           }       
       });
});