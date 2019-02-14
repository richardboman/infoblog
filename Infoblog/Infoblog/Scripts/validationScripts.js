$(function () {
    jQuery.validator.addMethod("noSpace", function (value, element) {
        return value.trim() != "";
    }, "Inte bara mellanslag, tack!");

    $('#meetingPollForm').validate({
        rules: {
            Title: {
                required: true,
                minlength: 2,
                maxlength: 25,
                noSpace: true
            },
            Content: {
                required: true,
                minlength: 5,
                maxlength: 100,
                noSpace: true
            }
        },
        messages: {
            Title: {
                required: "Du måste ha en titel!",
                minlength: "Titeln måste vara minst 2 tecken!",
                maxlength: "Titeln får inte vara längre än 25 tecken!"
            },
            Content: {
                required: "Skriv ett meddelande!",
                minlength: "Meddelandet måste vara minst 5 tecken!",
                maxlength: "Meddelandet får inte vara längre än 100 tecken!"
            }
        }
    });
});