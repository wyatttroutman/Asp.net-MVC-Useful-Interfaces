function HandleError(errorHeader, errorMessage) {
    $('#ErrorHeader').text(errorHeader);
    $('#ErrorMessage').text(errorMessage);
    $('.modal').modal('hide');
    $('#ErrorModal').modal('show');
}

function HandleInfo(infoHeader, infoMessage) {
    $('#InfoHeader').text(infoHeader);
    $('#InfoMessage').text(infoMessage);
    $('.modal').modal('hide');
    $('#InfoModal').modal('show');
}

function ShowErrorModal() {
    $('.modal').modal('hide');
    $('#ErrorModal').modal('show');
}

function ShowInfoModal() {
    $('.modal').modal('hide');
    $('#InfoModal').modal('show');
}