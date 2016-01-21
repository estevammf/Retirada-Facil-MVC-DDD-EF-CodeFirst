
//Make sure jQuery has been loaded before app.js
if (typeof jQuery === "undefined") {
    throw new Error("jQuery não foi carregado!");
}


$(function () {
    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {
        // hide dropdown if any (this is used wehen invoking modal from link in bootstrap dropdown )
        //$(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});

$(function () {
    $(".btn_search").click(function () {
        event.preventDefault();
        var data = this.href;
        $("#conteudoRendimentos").load(data);
    }); 
});

function buscaRendimentos(url) {
    
    $.ajax({
        url: url,
        data: {}, //parameters go here in object literal form
        type: 'GET',
        datatype: 'json',
        success: function (dados) { $('#conteudoRendimentos').load(dados); alert('got here with data'); },
        error: function () { alert('something bad happened'); }
    });



        //$.get(url, function (data) {
            
        //});
   
};


function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $(location).attr('href', result.url);
                   //$('#myModal').modal('hide');
                   // $('#replacetarget').load(result.url); //  Load data from the server and place the returned HTML into the matched element
                }
                if (result.mensagem != undefined) {
                    $('#myModal').modal('hide');
                    $(location).attr('href', result.url);
                    //$('#replacetarget').load(result.url);
                    //$('#mensagemErro').text(result.mensagem);
                }
                else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });
        return false;
    });
}

