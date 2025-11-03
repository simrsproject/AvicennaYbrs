<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StandardReferenceCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.StandardReferenceCtl" %>


<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboItemID" runat="server" Width="100%" EmptyMessage="Select a Item"
                                 EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                 OnClientItemsRequesting="cboItemID_ClientItemsRequesting" OnClientFocus="showDropDown">
                <WebServiceSettings Method="StandardReference" Path="~/WebService/ComboBoxDataService.asmx" />
            </telerik:RadComboBox>
            <asp:HiddenField runat="server" id="hdnReferenceID"/>
            <script type="text/javascript">
                (function (global, undefined) {
                    function cboItemID_ClientItemsRequesting(sender, eventArgs) {
                        var context = eventArgs.get_context();
                        context["RefID"] = document.getElementById("<%=hdnReferenceID.ClientID%>").value;
                    }

                    function showDropDown(sender, eventArgs) {
                        sender.showDropDown();
                        sender.requestItems("[showall]", false);
                    }

                    global.cboItemID_ClientItemsRequesting = cboItemID_ClientItemsRequesting;
                    global.showDropDown = showDropDown;
                })(window);
            </script>
        </td>
    </tr>
</table>
