
function alertOnAddCategory(categoryName) {
    if (categoryName.length > 19 || categoryName.length < 1) {
        alert("Error!\nInvalid category name, 1-19 letters required");
    } else {
        alert("Success!\n" + categoryName + " was successfully added");
    }
}

function alertOnUpdatePrice(price) {
    if (price < 1 || price > 999) {
        alert("Error!\nInvalid price, 1-999 is accepted");
    } else {
        alert("Success!\nPrice was successfully updated!");
    }
}

function alertOnSeed() {
    alert("Success!\nThe database was successsfully recreated!");
}

$("#newNameBtn").click(function() {
    if ($("#newNameInput").val() !== "") {
        //$("#updateNameForm").submit(function () {
        $.ajax({
            method: "PUT",
            url: "/Pie/RenamePie",
            data: { 'id': $("#pieId").val(), 'newName': $("#newNameInput").val() }
        }).done(function(msg) {
            $("#pieName").html(msg);
            $("#newNameInput").val("");
        });

    } else {
        alert("Input field is empty");
    }
}); 
