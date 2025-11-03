function openWindowMax(url, overflow)
{
  if (overflow == '' || !/^(scroll|resize|both)$/.test(overflow))
  {
    overflow = 'both';
  }
  var offset = (navigator.userAgent.indexOf("Mac") != -1 || 
                  navigator.userAgent.indexOf("Gecko") != -1 || 
                  navigator.appName.indexOf("Netscape") != -1) ? 0 : 4;

  var win = window.open(url, '',
      'width=' + screen.availWidth + (2 * offset) + ',height=' + screen.availHeight + (2 * offset)
      + ',scrollbars=' + (/^(scroll|both)$/.test(overflow) ?
      'yes' : 'no')
      + ',resizable=' + (/^(resize|both)$/.test(overflow) ?
      'yes' : 'no')
      + ',status=yes,toolbar=no,menubar=no,location=no'
  );
  return win;
}
