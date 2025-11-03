<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.LocalistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function entryLocalistStatus<%= SessionNameDtb %>(mod, rimid, bodyId, parid, unit) {
            var url = '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx?mod=' + mod + '&parid=' + parid + '&patid=<%= PatientID %>&rimid=' + rimid + '&regno=<%= RegistrationNo %>&unit=' + unit + '&bodyId=' + bodyId +'&sndtb=<%= SessionNameDtb %>&ccm=rebind&cet=<%=lvLocalistStatus.ClientID %>';
            window.openWinEntryMaxWindow(url);
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
<asp:HiddenField runat="server" ID="hdnReferenceID" />
<asp:HiddenField runat="server" ID="hdnNoteVisible" Value="1" />

<%--FORMAT VERTICAL --%>
<telerik:RadListView ID="lvLocalistStatus" runat="server" RenderMode="Lightweight"
    ItemPlaceholderID="BodyImageContainer" OnNeedDataSource="lvLocalistStatus_NeedDataSource">
    <LayoutTemplate>
        <fieldset  >
            <legend><b>LOCALIST STATUS</b></legend>
                    <asp:PlaceHolder ID="BodyImageContainer" runat="server"></asp:PlaceHolder>
        </fieldset>
    </LayoutTemplate>
    <ItemTemplate>
            <table <%=Width%> >
                <tr>
                    <td style="width: 210px">
                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit"
                            OnClientClick='<%# string.Format("entryLocalistStatus{5}(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");return false;", 
                                                        DataBinder.Eval(Container.DataItem, "EntryMode"),        
                                                        RegistrationInfoMedicID, 
                                                        DataBinder.Eval(Container.DataItem, "BodyID"),
                                                        ParamedicID,
                                                        ServiceUnitID,SessionNameDtb)%>'>
                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                Width="200px" Height="200px" ResizeMode="Fit" DataValue='<%# Eval("BodyImage") == DBNull.Value? new System.Byte[0]: Eval("BodyImage") %>'></telerik:RadBinaryImage>
                        </asp:LinkButton>
                    </td>
                    <td style="vertical-align: top">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="3"><%#DataBinder.Eval(Container.DataItem, "BodyName")%></td>
                            </tr>
                            <tr>
                                <td style="width: 150px;">Add:&nbsp;<%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute))%></td>
                                <td style="width: 150px;">Upd:&nbsp;<%#string.Format("{0}",Eval("LastUpdateDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("LastUpdateDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute))%></td>
                                <td></td>
                            </tr>
                        </table>
                        <telerik:RadTextBox runat="server" ID="txtNotes" Width="100%" Height="160px" Resize="Vertical" TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem, "Notes")%>'></telerik:RadTextBox>
                    </td>
                </tr>
            </table>
        <br/>
    </ItemTemplate>
</telerik:RadListView>

<%--FORMAT HOROZONTAL --%>
<%--    <telerik:RadListView ID="RadListView1" runat="server" RenderMode="Lightweight"
    ItemPlaceholderID="BodyImageContainer" OnNeedDataSource="lvLocalistStatus_NeedDataSource">
    <LayoutTemplate>
        <fieldset style="height: 230px; width: 49%;overflow: auto;">
            <legend>LOCALIST STATUS</legend>
            <table width="100%">
                <tr>
                    <asp:PlaceHolder ID="BodyImageContainer" runat="server"></asp:PlaceHolder>
                </tr>
            </table>
        </fieldset>

    </LayoutTemplate>
    <ItemTemplate>
        <td>
            <table style="width: 100%; border: 1px solid gray;">
                <tr>
                    <td style="width: 210px">
                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit"
                            OnClientClick='<%# string.Format("entryLocalistStatus(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");return false;", 
                                                        DataBinder.Eval(Container.DataItem, "EntryMode"),        
                                                        RegistrationInfoMedicID, 
                                                        DataBinder.Eval(Container.DataItem, "BodyID"),
                                                        ParamedicID,
                                                        ServiceUnitID)%>'>
                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                Width="200px" Height="200px" ResizeMode="Fit" DataValue='<%# Eval("BodyImage") == DBNull.Value? new System.Byte[0]: Eval("BodyImage") %>'></telerik:RadBinaryImage>
                        </asp:LinkButton>
                    </td>
                    <td style="vertical-align: top">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="3"><%#DataBinder.Eval(Container.DataItem, "BodyName")%></td>
                            </tr>
                            <tr>
                                <td style="width: 150px;">Add:&nbsp;<%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute))%></td>
                                <td style="width: 150px;">Upd:&nbsp;<%#string.Format("{0}",Eval("LastUpdateDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("LastUpdateDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute))%></td>
                                <td></td>
                            </tr>
                        </table>
                        <telerik:RadTextBox runat="server" ID="txtNotes" Width="100%" Height="160px" Resize="Vertical" TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem, "Notes")%>'></telerik:RadTextBox>
                    </td>
                </tr>
            </table>
        </td>
    </ItemTemplate>
    </telerik:RadListView>--%>
