<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssessmentImageCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.AssessmentImageCtl" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<telerik:RadAjaxManagerProxy ID="ajxProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdImageGallery">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdImageGallery" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">


</script>
</telerik:RadCodeBlock>
<fieldset style="width: 49%;">
    <legend><b>Image Document</b></legend>
    <telerik:RadGrid ID="grdImageGallery" Width="100%" GridLines="None" runat="server" ShowHeader="false" 
        OnInsertCommand="grdImageGallery_OnInsertCommand"
        OnUpdateCommand="grdImageGallery_OnUpdateCommand"
        OnDeleteCommand="grdImageGallery_OnDeleteCommand"
        OnNeedDataSource="grdImageGallery_OnNeedDataSource"
        OnItemCommand="grdImageGallery_OnItemCommand">

        <MasterTableView CommandItemDisplay="None" DataKeyNames="ImageNo" 
            HorizontalAlign="NotSet" AutoGenerateColumns="False">
            <CommandItemSettings AddNewRecordImageUrl="~/Images/Toolbar/insert16.png" AddNewRecordText="Add"></CommandItemSettings>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="ImageNo" SortOrder="Ascending" />
            </SortExpressions>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Toolbar/edit16.png" HeaderStyle-Width="30px"></telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="130px" UniqueName="DocumentImage" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="Zoom"
                            OnClientClick='<%#string.Format("javascript:AssessmentImageGallery(\"{0}\",{1});return false;",DataBinder.Eval(Container.DataItem, "RegistrationInfoMedicID"),DataBinder.Eval(Container.DataItem, "ImageNo"))%>'>
                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("DocumentImage") == DBNull.Value? new System.Byte[0]: Eval("DocumentImage") %>'></telerik:RadBinaryImage>
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Description" UniqueName="DocumentName" >
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "DocumentName")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/AssessmentImageItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="AssessmentImageItemEditCommand">
                </EditColumn>
            </EditFormSettings>

        </MasterTableView>
    </telerik:RadGrid>
</fieldset>

