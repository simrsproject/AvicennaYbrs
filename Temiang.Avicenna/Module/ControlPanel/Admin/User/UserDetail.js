
(function (global, undefined) {
    var ajaxManagerID;
    function onClientFilesUploaded(sender, args) {
        $find(ajaxManagerID).ajaxRequest();
    }
    function serverID(name, id) {
        ajaxManagerID = id;
    }

    global.serverID = serverID;
    global.OnClientFilesUploaded = onClientFilesUploaded;


})(window);