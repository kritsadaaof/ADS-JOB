$(document).ready(function () {
    $("#Find_Candidates").click(function () {
            $.ajax({
                type: "post",
                url: baseUrl + "ManageCandidates/DataFindByDepart",
                data: { DEPART: $("#DepartmentS").val()},
                success: function (data) {
                    if (data != "") {
                        var pr = $.parseJSON(data);
                        window.location = baseUrl + "ManageCandidates/FindByDepart";
                    }
                    else {
                        var nFrom = "bottom";
                        var nAlign = "center";
                        var nIcons = $(this).attr('data-icon');
                        var nType = "warning";
                        var nAnimIn = $(this).attr('data-animation-in');
                        var nAnimOut = $(this).attr('data-animation-out');
                        var mEss = "กรุณาเลือกข้อมูลให้ถูกต้อง";
                        notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
                        setTimeout(
                            function () {
                                window.location = baseUrl + "ManageCandidates/Manage_Candidates";
                            }, 1500);
                      
                    }
                    
                }
            });     
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


/*
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
            y: 10
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
*/