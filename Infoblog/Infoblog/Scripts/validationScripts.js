$(function () {
    jQuery.validator.addMethod("noSpace", function (value, element) {
        return value.trim() != "";
    }, "Inte bara mellanslag, tack!");

    jQuery.validator.addMethod("notDefaultHour", function (value, element) {
        return value.trim() != "Timmar";
    }, "Vänligen välj en tid!");

    jQuery.validator.addMethod("notDefaultMinutes", function (value, element) {
        return value.trim() != "Minuter";
    }, "Vänligen välj en tid!");

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
            },
            meetingDate: {
                required: true
            },
            startHours: {
                required: true,
                notDefaultHour: true
            }, 
            startMinutes: {
                required: true,
                notDefaultMinutes: true
            },
            endHours: {
                required: true,
                notDefaultHour: true
            }, 
            endMinutes: {
                required: true,
                notDefaultMinutes: true
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
            }, 
            meetingDate: {
                required: "Du måste välja ett datum!"
            }, 
            startHours: {
                required: "Du måste välja en starttid!"
            }, 
            startMinutes: {
                required: "Du måste välja en starttid!"
            }, 
            endHours: {
                required: "Du måste välja en sluttid!"
            }, 
            endMinutes: {
                required: "Du måste välja en sluttid!"
            }
        }
    });
});