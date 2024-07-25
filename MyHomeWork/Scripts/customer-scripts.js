var editMode = false;

function convertMongoDateToHTMLDate(mongoDate) {
    var milliseconds = parseInt(mongoDate.match(/\d+/)[0]);
    var date = new Date(milliseconds);
    var htmlDate = date.toISOString().slice(0, 19).replace('T', ' '); // YYYY-MM-DD HH:mm:ss
    return htmlDate;
}

function convertHTMLDateToMongoDate(htmlDate) {
    var date = new Date(htmlDate);
    var milliseconds = date.getTime();
    var mongoDate = "/Date(" + milliseconds + ")/";
    return mongoDate;
}

function generateUUID() {
    var d = new Date().getTime();
    if (typeof performance !== 'undefined' && typeof performance.now === 'function') d += performance.now(); //use high-precision timer if available
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}

function generateId() {
    return new Promise((resolve, reject) => {
        var uuid = generateUUID();
        $.ajax({
            type: "GET",
            url: 'CheckCustomer/Customers',
            data: { customerId: uuid },
            success: function (data) {
                data === "true" ? resolve(uuid) : reject('Customer Id does not exist!');
            }
        });
    });
}

$(function() {
    var defaultConfig = {
        autoOpen: false,
        resizable: false,
        modal: true,
        width: 700,
    };
    $("#dialog_form").dialog(defaultConfig);
    $("#dialog_delete").dialog(defaultConfig);
});

$(document).ready(function () {
    $('#table_list').DataTable(
        {
            "paging": true,
            "ordering": false,
            "info": true,
            "searching": true,
            "lengthChange": true,
            "autoWidth": true,
            "responsive": true,
            "pageLength": 5,
            "lengthMenu": [5, 10, 25, 50, 75, 100],
        }
    );

    $(document).on('click', '.edit-button', function () {
        var row = $(this).closest('tr');
        var customerId = row.find('td:eq(0)').text();
        $.ajax({
            type: "GET",
            url: 'Customers/GetCustomer',
            data: { customerId: customerId },
            success: function (data) {
                console.log(data);
                $('#IdRequest').val('1'); // 1 = Edit
                var CustomerId = $('#CustomerId');
                CustomerId.val(data.CustomerId);
                $('#FirstName').val(data.FirstName);
                $('#LastName').val(data.LastName);
                $('#Company').val(data.Company);
                $('#City').val(data.City);
                $('#Country').val(data.Country);
                $('#Phone1').val(data.Phone1);
                $('#Phone2').val(data.Phone2);
                $('#Email').val(data.Email);
                $('#SubscriptionDate').val(convertMongoDateToHTMLDate(data.SubscriptionDate));
                $('#Website').val(data.Website);
            }
        });
        openDialog('dialog_form');
        editMode = true;
    });

    $(document).on('click', '.delete-button', function () {
        var row = $(this).closest('tr');
        var customerId = row.find('td:eq(0)').text();
        $('#CustomerIdDelete').val(customerId);
        openDialog('dialog_delete');
    });

    $(document).on('click', '.create-button', function () {
        var CustomerId = $('#CustomerId');
        CustomerId.val(generateUUID());
        $('#IdRequest').val('0'); // 0 = Create
        $('#FirstName').val('');
        $('#LastName').val('');
        $('#Company').val('');
        $('#City').val('');
        $('#Country').val('');
        $('#Phone1').val('');
        $('#Phone2').val('');
        $('#Email').val('');
        $('#SubscriptionDate').val('');
        $('#Website').val('');
        openDialog('dialog_form');
        editMode = false;
    });

    setInterval(function () {
        var customerId = $('#CustomerId').val();
        if (editMode) return;
        $.ajax({
            type: "GET",
            url: 'CheckCustomer/Customers',
            data: { customerId: customerId },
            success: function (data) {
                if (data === "true") {
                    $('#CustomerId').css('border-color', 'red');
                    $('.confirm-button').prop('disabled', true);
                    $('#error-handler').text('[Customer Id] already exists, please generate a new one!');
                }
                else {
                    $('#CustomerId').css('border-color', '');
                    $('.confirm-button').prop('disabled', false);
                    $('#error-handler').text('');
                }
            }
        });
    }, 1000);
});

function openDialog(id) {
    $('#' + id).dialog('open');
}

function closeDialog(id) {
    $('#' + id).dialog('close');
}
