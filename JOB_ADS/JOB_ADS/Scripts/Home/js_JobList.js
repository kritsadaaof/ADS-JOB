$(document).ready(function () {
    document.getElementById('CheckJobList').style.display = 'none';
    document.getElementById('Regist').style.display = '';


    $('#CVUplode').change(function () {
        if ($('#CVUplode').val() == "") {
            $('#CV').html('Browse CV');
        } else {
            var res = $('#CVUplode').val().substring(12);
            $('#CV').html(res);
        }
    });
    $('#PICUplode').change(function () {
        if ($('#PICUplode').val() == "") {
            $('#PIC').html('Browse Pic');
        }
        else {
            var res = $('#PICUplode').val().substring(12);
            $('#PIC').html(res);
        }
    });
    $('#CERUplode').change(function () {
        if ($('#CERUplode').val() == "") {
            $('#CER').html('Browse Certificate');
        }
        else {
            var res = $('#CERUplode').val().substring(12);
            $('#CER').html(res);
        }
    });
    // $('.timepicker').pickatime({});
    
     

    //$("#timepicker1").timepicker(); 
    //**********************เพิ่ม***********************************
    //$(".Date").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
   // $(".Date").val(val);

   // $("#Date").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
  //  $("#Date").val(val);


    $("#SavePostjob").click(function () {
        var nFrom = "bottom";
        var nAlign = "center";
        var nIcons = $(this).attr('data-icon');
        var nType = "success";
        var nAnimIn = $(this).attr('data-animation-in');
        var nAnimOut = $(this).attr('data-animation-out');
        var mEss = "บันทึกข้อมูลสำเร็จ";
        notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
        setTimeout(
            function () {
                location.reload();
            }, 4000);
    });

    $("#Clear").click(function () {
         
        location.reload();  
    });

    $("#OK").click(function () { 
        $("#CVUplode").click();
    });

    $("#formUP").click(function () {
        $("#exampleModalLong").modal('show'); 
    });
    $("#ApplyJobList").click(function () {

        //  $('#tab_logic').append('<tr id="addr' + (1) + '"><td>aaa</td></tr>');
        //  document.getElementById('FormPDConfirm').style.display = '';
        //  alert(x);
        // window.location = baseUrl + "Home/JobList?JOB=" + $('#TitleS').val();

        const checkboxes = document.querySelectorAll('.z:checked');
        let colors = [];
        var a = 1;
        checkboxes.forEach((checkbox) => {
            colors.push(checkbox.value);

            //////////////////////////////////////////////////////////////////////
            $.post(baseUrl + "Home/LoadRegistJoblist", {
                IDJob: checkbox.value
            }).done(function (data) {
                // alert(data);
                var pr = $.parseJSON(data);
               // alert(pr);
                $.each(JSON.parse(data), function (i, obj) {
                    
                    $('#tab_logic').append('<tr id="addr' + (a) + '"><td>' + (a) + '</td><td>' + pr[i]["JOB_Title"] + '</td><td>' + pr[i]["JOB_Region"] +
                        '</td><td>' + pr[i]["JOB_Location"] + '</td><td>' + pr[i]["Salary"] + '</td><td>' + " <div class='input-group'><input type='text'class='form-control py-2 px-4' id='Ex" + a + "'><div class='input-group-append'><span class='input-group-text px-2'>ปี</span></div></div>" + '</td></tr>');
                  // a = a + 1;
                }); 
                a++;
                

            });
        });
       
        /////////////////////////////////////////////////////////////////////
        

        if (colors != '') { 
                    document.getElementById('CheckJobList').style.display = 'none';
                    document.getElementById('Regist').style.display = ''; 
        }
    });
    // alert(colors);
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
 