function OpenModal(id)
{
    id = "#" + id;
    $(id).modal('show');
}

function CloseModal(id) {
    id = "#" + id;
    $(id).modal('hide');
}

function SwitchToTab(tabname)
{
    $('.nav-tabs a[href="#' + tabname + '"]').tab('show');
}

function GetPleaseWait() {
    return "<i class='fa fa-spinner'></i> Please wait..."
}