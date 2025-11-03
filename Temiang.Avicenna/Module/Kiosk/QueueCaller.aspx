<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="QueueCaller.aspx.cs" Inherits="Temiang.Avicenna.QueueCaller" Title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .btnNext {
            Width: 90px; 
        }
        .btnNextSmall {
            Width: 60px;
            height:25px;
            font: bold 12px "helvetica neue", helvetica, arial, sans-serif;
            padding: 5px 0 12px 0;
        }
    </style>
    <script type="text/javascript">
        var _fnGetDataIsRunning = false;
        var _UserID = "<%=getUserID() %>";
        
        var BASEUrl = '<%=Page.ResolveUrl("~/")%>';
        
        $ = $telerik.$; 
            
        $(document).ready(function () {

            $.ajaxSetup({
                beforeSend: function (xhr) {
                    if (xhr.overrideMimeType) {
                        xhr.overrideMimeType("application/json");
                    }
                }
            });

            setInterval(function() {
                //alert("Hooray! The document is ready!");
                getLastData();
            }, 5000);
        });
        
        function getLastData(){
            if(_fnGetDataIsRunning == true) return;
            
            _fnGetDataIsRunning = true;
            //var kode = ['A','B'];
            var kode = GetCodeAll(); //GetCodeSelected();
            if(kode.length < 1){
                _fnGetDataIsRunning = false;
                return;
            }
            
            $.ajax({
                type: 'POST',
                url: BASEUrl + 'WebService/KioskQueue.asmx/GetLastQueue',
                data: { jSonKioskQueueType: JSON.stringify({ kode }), UserID: _UserID },
                success: function(ret) {
                    // do something
                    //alert(ret);
                    if (ret.status === 'OK') {
                        ParsingQueueLast(ret.data);
                    } else {
                        alert(ret.data);
                    }

                    _fnGetDataIsRunning = false;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    _fnGetDataIsRunning = false;
                    alert(xhr.responseText);
                },
                dataType: 'json'
            });
        }

        function chkHClicked(o) {
            //alert(o);
            var grid = $find("<%=grdQueueType.ClientID %>");
            var masterTableView = grid.get_masterTableView();
            var dataItems = masterTableView.get_dataItems();
            for (var i = 0; i < dataItems.length; i++) {
                var gridItemElement = dataItems[i].findElement("chkQueueType");
                gridItemElement.checked = o.checked;
            }
        }
        
        function GetCodeSelected(){
            var selected = [];
        
            var grid = $find("<%=grdQueueType.ClientID %>");
            var masterTableView = grid.get_masterTableView();
            var dataItems = masterTableView.get_dataItems();
            for(var i = 0; i < dataItems.length; i++){
                var gridItemElement = dataItems[i].findElement("chkQueueType");
                var isChecked = gridItemElement.checked;

                var key = dataItems[i].getDataKeyValue("ItemID");

                if(isChecked){
                    //alert(key);
                    selected.push(key);
                    $("#btnNextSmall" + key).show();
                }else{
                    //var txtLast = dataItems[i].findControl("txtLast");
                    //txtLast.set_value('');
                    
                    //var txtNext = dataItems[i].findControl("txtNext");
                    //txtNext.set_value('');

                    $("#btnNextSmall" + key).hide();
                }
            }
            
            return selected;
        }
        function GetCodeAll() {
            var selected = [];

            var grid = $find("<%=grdQueueType.ClientID %>");
            var masterTableView = grid.get_masterTableView();
            var dataItems = masterTableView.get_dataItems();
            for (var i = 0; i < dataItems.length; i++) {
                var gridItemElement = dataItems[i].findElement("chkQueueType");
                var isChecked = gridItemElement.checked;

                var key = dataItems[i].getDataKeyValue("ItemID");

                selected.push(key);

                if (isChecked) {
                    $("#btnNextSmall" + key).show();
                } else {
                    $("#btnNextSmall" + key).hide();
                }
            }

            return selected;
        }
        
        function ParsingQueueLast(oData){
            var grid = $find("<%=grdQueueType.ClientID %>");
            var masterTableView = grid.get_masterTableView();
            var dataItems = masterTableView.get_dataItems();
            
            var o = oData[0];
            for(var i = 0; i < o.length; i++){
                for (var j = 0; j < dataItems.length; j++) {
                    if(dataItems[j].getDataKeyValue("ItemID") == o[i].kioskQueueType){
                        var txtLast = dataItems[j].findControl("txtLast");
                        txtLast.set_value(o[i].last);
                        
                        var txtNext = dataItems[j].findControl("txtNext");
                        txtNext.set_value(o[i].next);

                        var txtCount = dataItems[j].findControl("txtCount");
                        txtCount.set_value(o[i].count);
                        break;
                    }
                }
            }
            
            //$.find("spanCurrent").innerHTML = oData[1].currentNo;
            //alert(oData[1]);
            //var curr = JSON.parse(oData[1]);
            //console.log(oData[1][0]);
            document.getElementById("spanCurrent").innerHTML = oData[1][0].KioskQueueNo;
        }
        
        var _fnNextIsRunning = false;
        function NextQueueByKode(kode) {
            if (GetCounterName() == "") alert("Counter name can not be empty");
            // update yang current, booking yang baru
            if(_fnNextIsRunning == true) return false;
            
            _fnNextIsRunning = true;
            
            var kodeArray = [kode];
            DoNextQueue(kodeArray);
        }
        function NextQueue() {
            if (GetCounterName() == "") alert("Counter name can not be empty");
            // update yang current, booking yang baru
            if(_fnNextIsRunning == true) return false;
            
            _fnNextIsRunning = true;
            //var kode = ['A','B'];
            var kode = GetCodeSelected();
            if(kode.length < 1){
                _fnNextIsRunning = false;
                return false;
            }
            DoNextQueue(kode);
        }
        function DoNextQueue(kode){
            var curr = document.getElementById("spanCurrent").innerHTML;

            $.ajax({
                type: 'POST',
                url: BASEUrl + 'WebService/KioskQueue.asmx/NextQueue',
                data: { jSonKioskQueueType: JSON.stringify({ kode }), CurrentNo: curr, UserID: _UserID },
                success: function(ret) {
                    // do something
                    //alert(ret);
                    if (ret.status === 'OK') {
                        ParsingQueueLast(ret.data);
                    } else {
                        alert(ret.data);
                    }

                    _fnNextIsRunning = false;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    _fnNextIsRunning = false;
                    alert(xhr.responseText);
                },
                dataType: 'json'
            });
            
            return false;
        }
        function RecallQueue() {
            var curr = document.getElementById("spanCurrent").innerHTML;
            $.ajax({
                type: 'POST',
                url: BASEUrl + 'WebService/KioskQueue.asmx/RecallQueue',
                data: { CurrentNo: curr, UserID: _UserID },
                success: function (ret) {
                    if (ret.status === 'OK') {
                        
                    } else {
                        alert(ret.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                },
                dataType: 'json'
            });
        }
        function StopQueue() {
            $.ajax({
                type: 'POST',
                url: BASEUrl + 'WebService/KioskQueue.asmx/StopQueue',
                data: { UserID: _UserID },
                success: function (ret) {
                    if (ret.status === 'OK') {
                        alert("Done");
                    } else {
                        alert(ret.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                },
                dataType: 'json'
            });
        }
        function SaveCounterName() {
            var cName = GetCounterName();
            $.ajax({
                type: 'POST',
                url: BASEUrl + 'WebService/KioskQueue.asmx/SaveCounterName',
                data: { CounterName: cName, UserID: _UserID },
                success: function (ret) {
                    if (ret.status === 'OK') {
                        alert("Done");
                    } else {
                        alert(ret.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                },
                dataType: 'json'
            });
            return false;
        }
        function GetCounterName() {
            return $find("<%=txtCounterName.ClientID %>").get_value();
        }

        function ClearQueuePrevDays() {
            var kode = GetCodeSelected();
            if (kode.length < 1) {
                return;
            }

            $.ajax({
                type: 'POST',
                url: BASEUrl + 'WebService/KioskQueue.asmx/QueueClear',
                data: { jSonKioskQueueType: JSON.stringify({ kode }), UserID: _UserID },
                success: function (ret) {
                    // do something
                    //alert(ret);
                    if (ret.status === 'OK') {
                        alert(ret.data + ' item(s) successfully cleaned');
                        getLastData();
                    } else {
                        alert(ret.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                },
                dataType: 'json'
            });
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2">
                <fieldset>
                    <legend>Queue Group Selection</legend>
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="cblQueueGroup" runat="server" RepeatDirection="Horizontal">

                                </asp:CheckBoxList>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="~/Images/Toolbar/search16.png" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="width:500px; vertical-align:top;">
                <telerik:RadGrid ID="grdQueueType" runat="server" OnNeedDataSource="grdQueueType_NeedDataSource"
                    OnItemDataBound="grdQueueType_ItemDataBound"
                    AutoGenerateColumns="false" AllowPaging="false" PageSize="15">
                    <MasterTableView DataKeyNames="ItemID" ClientDataKeyNames="ItemID">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="chk" HeaderStyle-Width="50px"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHQueueType" runat="server" Checked="false" OnClick="chkHClicked(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkQueueType" runat="server" Checked="false" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Queue Type" DataField="ItemName"
                                UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" 
                                ItemStyle-HorizontalAlign="Left" />
                            
                            <telerik:GridTemplateColumn UniqueName="Count" HeaderStyle-Width="35px" HeaderText = "Count"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtCount" runat="server" Width="33px"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="Next" HeaderStyle-Width="60px" HeaderText = "Next"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtNext" runat="server" Width="55px"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridTemplateColumn UniqueName="TopEnd" HeaderStyle-Width="60px" HeaderText = "End"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtLast" runat="server" Width="55px"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="btnNext" HeaderStyle-Width="80px" HeaderText = ""
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <button id="btnNextSmall<%# string.Format("{0}",DataBinder.Eval(Container.DataItem, "ItemID")) %>" class="punch btnNextSmall" 
                                        onclick="javascript: NextQueueByKode(<%# string.Format("'{0}'",DataBinder.Eval(Container.DataItem, "ItemID")) %>); return false;">NEXT</button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
            <td valign="top">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <fieldset>
                                <legend>Counter Name</legend>
                                <telerik:RadTextBox ID="txtCounterName" runat="server" Width="100px"></telerik:RadTextBox>
                                <button id="btnSaveCounterName" class="punch btnNextSmall" onclick="javascript: SaveCounterName(); return false;">Save</button>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <fieldset>
                                <legend>Current Number</legend>
                                <table>
                                    <tr>
                                        <td colspan="3"><span id="spanCurrent" style="font-size:72pt;">0</span></td>
                                    </tr>
                                    <tr>
                                        <td><button id="btnStop" class="punch btnNext" onclick="javascript: StopQueue(); return false;">STOP</button></td>
                                        <td><button id="btnRecall" class="punch btnNext" onclick="javascript: RecallQueue(); return false;">ReCALL</button></td>
                                        <td><button id="btnNext" class="punch btnNext" onclick="javascript: NextQueue(); return false;">NEXT</button></td>
                                    </tr>
                                </table>
                                
                            </fieldset>
                            <fieldset>
                                <legend>Tools</legend>
                                <table>
                                    <tr>
                                        <td>
                                            <button id="btnClearPrev" class="punch btnNext" onclick="javascript: ClearQueuePrevDays(); return false;" title="To clean the previous queue">Clear</button>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
