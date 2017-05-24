
function alertOnAddCategory(categoryName) {
    if (categoryName.length > 19 || categoryName.length < 1) {
        return openModal("Error", "Invalid category name, 1-19 letters required");
    } else {
        $("#categoryNameInput").val("");
        return openModal("Success", categoryName + " was successfully added");
    }
}

function alertOnUpdatePrice(price) {
    if (price < 1 || price > 999) {
        openModal("Error", "Invalid price, 1-999 is accepted");
    } else {
        openModal("Success", "Price was successfully updated!");
    }
}

function openModal(title, message) {
    $("#alertMessage").find(".modal-title").html(title);
    $("#alertMessage").find(".modal-body").html(message);
    $("#alertMessage").modal("show");
    return onModalClose();
}

function onModalClose() {
    let bajs = false;
    $.when($("#closeModal").preventDefault().click().done(function() {
            return false;
        })
)};