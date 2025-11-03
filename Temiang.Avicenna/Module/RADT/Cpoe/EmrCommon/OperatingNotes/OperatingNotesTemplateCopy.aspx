<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="OperatingNotesTemplateCopy.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EmrCommon.OperatingNotes.OperatingNotesTemplateCopy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<telerik:RadCodeBlock ID="radCodeBlock" runat="server">

<script type="text/javascript" language="javascript">
    function deleteTemplate(rowIndex) {
        if (confirm('This action will delete the selected template. Are you sure?')) {
            var masterTable = $find("<%= grdList.ClientID %>").get_masterTableView();
            masterTable.fireCommand('Delete', rowIndex);
        }
    }
</script>

</telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="txTemplateText" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="txTemplateText" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="label">
                <asp:Label ID="lblAssetName" runat="server" Text="Search"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSearch" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
    OnClick="btnSearch_Click" />
</td>
<td></td>
</tr>
</table>
<hr/>
<table style="width: 100%">
    <tr>
        <td valign="top" style="width:30%">
<telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" 
    AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="10"
    AllowSorting="True" GridLines="None" OnSelectedIndexChanged="grdList_SelectedIndexChanged" OnItemCommand="grdList_OnItemCommand">
       
<MasterTableView DataKeyNames="TemplateID">
    <Columns>
        <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="TemplateID" HeaderText="ID" 
    UniqueName="TemplateID" SortExpression="Code" HeaderStyle-HorizontalAlign="Center"
    ItemStyle-HorizontalAlign="Center" Display="False" />
<telerik:GridBoundColumn DataField="TemplateName" HeaderText="Name" UniqueName="TemplateName"
    SortExpression="TemplateName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
    <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
    <ItemStyle VerticalAlign="Top"></ItemStyle>
    <ItemTemplate>
            <%# string.Format("<a href=\"#\" onclick=\"javascript:deleteTemplate({0}); return false;\"><img src=\"../../../../../Images/Toolbar/row_delete16.png\"  /></a>",Container.ItemIndex)%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>
</Columns>
</MasterTableView>
<ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
    <Selecting AllowRowSelect="true" />

</ClientSettings>
</telerik:RadGrid>
    </td>
    <td valign="top">
        <fieldset style="width: 96%">
            <legend>Operating Note</legend>
            <telerik:RadTextBox ID="txTemplateText" runat="server" Width="100%" Height="285px" 
    TextMode="MultiLine" />
</fieldset>
</td>
</tr>

</table>
</asp:Content>