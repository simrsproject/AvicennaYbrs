<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitBookingBodyImageCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ServiceUnitBookingBodyImageCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function entryLocalistStatus(mod, bodyId) {
            var url = '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx?mod=' + mod + '&bodyId=' + bodyId + '&ccm=rebind&cet=<%=lvLocalistStatus.ClientID %>';
            window.openWindow(url, 800, 620);
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="lvLocalistStatus">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lvLocalistStatus" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<asp:HiddenField runat="server" ID="hdnBookingNo" />
<asp:HiddenField runat="server" ID="hdnOpNotesSeqNo" />
<asp:HiddenField runat="server" ID="hdnServiceUnitID" />
<telerik:RadListView ID="lvLocalistStatus" runat="server" RenderMode="Lightweight"
    ItemPlaceholderID="BodyImageContainer" OnNeedDataSource="lvLocalistStatus_NeedDataSource">

    <LayoutTemplate>
        <fieldset style="height: 150px; width: 400px;overflow: auto;">
            <legend>LOCALIST STATUS</legend>
            <table>
                <tr>
                    <asp:PlaceHolder ID="BodyImageContainer" runat="server"></asp:PlaceHolder>
                </tr>
            </table>
        </fieldset>    </LayoutTemplate>
    <ItemTemplate>
        <td style="height: 125px; width: 225px;">
            <table>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit"
                            OnClientClick='<%# string.Format("entryLocalistStatus(\"{0}\", \"{1}\");return false;", 
                                                        DataBinder.Eval(Container.DataItem, "EntryMode"),        
                                                        DataBinder.Eval(Container.DataItem, "BodyID"))%>'>
                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("BodyImage") == DBNull.Value? new System.Byte[0]: Eval("BodyImage") %>'></telerik:RadBinaryImage>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2"><%#DataBinder.Eval(Container.DataItem, "BodyName")%></td>
                            </tr>
                            <tr>
                                <td style="width: 50px;">Add:</td>
                                <td><%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.Date))%></td>
                            </tr>
                            <tr>
                                <td>Upd:</td>
                                <td><%#string.Format("{0}",Eval("LastUpdateDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("LastUpdateDateTime")).ToString(AppConstant.DisplayFormat.Date))%></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </ItemTemplate>
</telerik:RadListView>
