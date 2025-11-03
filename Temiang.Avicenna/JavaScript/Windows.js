var popupWindow;
function openPopupCenter(url, width, height)
{
    if (width > 640) { width = 640; }
    if (height > 480) { height = 480; }
  
    var left = parseInt((screen.availWidth/2) - (width/2));
    var top = parseInt((screen.availHeight/2) - (height/2));
    var windowFeatures = "width=" + width + ",height=" + height +
        ",resizable=yes, scrollbars=no, toolbar=no, location=no, directories=no, status=no, menubar=no,left=" + left + ",top=" + top +
        ",screenX=" + left + ",screenY=" + top;
    popupWindow = window.open(url, "popupWind", windowFeatures);
    popupWindow.focus();
}