$(document).ready(function () {
    let editor = CodeMirror.fromTextArea(document.getElementById("Code"), {
        lineNumbers: true,
        matchBrackets: true,
        mode: "text/x-csharp",
        theme: "dracula"
    });

    $("#submit").click(function (e) {
        $.ajax({
            url: '/DotNet/ExecuteCode/',
            type: "POST",
            dataType: 'json',
            data: {code : editor.getValue() },
            success: function(data){
                $('#results').empty();
                let json = JSON.parse(data);
                let items = json.Values;
                for (let i = 0; i < items.length; i++) {
                    $('#results').append('<p>' + '' + items[i] + '</p>');
                }
            }
        });
    });
});