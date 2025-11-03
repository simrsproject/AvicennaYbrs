/* 
 * MAIN TIC TOC
 */

var currentTic = 0;
var msTic = 1000;
var _FNTOEXEC = [];
/*  sample how to push function to main timer
    push fn to main tic fn
    var args = [param1, param2, ...];
    var jSonFnTic = {fn: FnToExecute, args: args, tic: 5000};
    _FNTOEXEC.push(jSonFnTic);
 */

$(document).ready(function() {
    //MainFnExec(); // exec all 
    MainTicFnStart();
});

function MainTicFnStart(){
    setInterval(function() {
        for(var i = 0; i < _FNTOEXEC.length; i++){
            // run fn
            if((msTic * currentTic) % _FNTOEXEC[i].tic === 0){
                // exec
                var doExec = partial(_FNTOEXEC[i].fn, _FNTOEXEC[i].args);
                doExec(_FNTOEXEC[i].args);
            }
        }
        currentTic++;
        //console.log("tic");
    }, msTic);
}
