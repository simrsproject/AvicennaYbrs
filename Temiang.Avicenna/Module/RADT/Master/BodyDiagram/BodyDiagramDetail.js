
(function (global, undefined) {
    var ajaxManagerID;
    var demo = {};
    function onClientFilesUploaded(sender, args) {
        $find(ajaxManagerID).ajaxRequest();
    }
    function serverID(name, id) {
        demo[name] = id;
        ajaxManagerID = id;
    }

    global.serverID = serverID;
    global.OnClientFilesUploaded = onClientFilesUploaded;


})(window);