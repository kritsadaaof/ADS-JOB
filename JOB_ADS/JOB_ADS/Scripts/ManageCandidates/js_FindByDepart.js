$(document).ready(function () {
    $.post(baseUrl + "ManageCandidates/LoadDataDepart", {
        DEPART: $('<%= Session("DEPARTMENT")').val()
            }).done(function (data) {
                if (data != "[]") {
                    var pr = $.parseJSON(data);
                    $.each(JSON.parse(data), function (i, obj) {

                        $('#table-basic').dataTable().fnAddData([
                            (i + 1),
                            pr[i]["Position"],
                            pr[i]["Number"]

                        ]);
                    });

                }
                else {
                 
                }


               
            });

        
       
   
});

function notify(from, align, icon, type, animIn, animOut, mEssage) { //Notify
    $.growl({
        icon: icon,
        title: ' แจ้งเตือน ',
        message: mEssage,

        url: ''
    }, {
        element: 'body',
        type: type,
        allow_dismiss: true,
        placement: {
            from: from,
            align: align
        },
        offset: {
            x: 20,
            y: 85
        },
        spacing: 10,
        z_index: 1031,
        delay: 2500,
        timer: 2000,
        url_target: '_blank',
        mouse_over: false,
        animate: {
            enter: animIn,
            exit: animOut
        },
        icon_type: 'class',
        template: '<div data-growl="container" class="alert" role="alert">' +
            '<button type="button" class="close" data-growl="dismiss">' +
            '<span aria-hidden="true">&times;</span>' +
            '<span class="sr-only">Close</span>' +
            '</button>' +
            '<span data-growl="icon"></span>' +
            '<span data-growl="title"></span>' +
            '<span data-growl="message"></span>' +
            '<a href="#" data-growl="url"></a>' +
            '</div>'
    });
};

