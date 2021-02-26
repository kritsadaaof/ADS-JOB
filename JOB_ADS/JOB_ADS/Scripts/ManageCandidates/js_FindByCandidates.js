$(document).ready(function () {
    $("#Clear").click(function () {
        location.reload();
    });
    $("#Selected").click(function () {
        const checkboxes = document.querySelectorAll('.CCandidates:checked');
        let colors = [];
        var a = 1;
        checkboxes.forEach((checkbox) => {
            colors.push(checkbox.value);
            $.post(baseUrl + "ManageCandidates/LoadRegistCandidates", {
                IDCandidates: checkbox.value
            }).done(function (data) {
                
                var pr = $.parseJSON(data);
             
                $.each(JSON.parse(data), function (i, obj) {

                    $('#tab_select').append('<tr id="addr' + (a) + '"><td>' + (a) + '</td><td>' + pr[i]["Re_Title_TH"] + '</td><td>' + pr[i]["Re_Name_TH"] +
                        '</td><td>' + pr[i]["Re_Surname_TH"] + '</td></tr>');
                    
                });
                a++;


            });
        });

        


        //if (colors != '') {
           //document.getElementById('CheckJobList').style.display = 'none';
           //document.getElementById('Regist').style.display = '';
        //}
   
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

