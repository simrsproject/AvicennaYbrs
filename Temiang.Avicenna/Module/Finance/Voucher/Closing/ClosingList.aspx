<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ClosingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.Closing.ClosingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            function openPosting(PostingId) {
                if (confirm('Are you sure want to unlock selected item?')){
                    __doPostBack("<%= grdList.UniqueID %>", 'openPosting|' + PostingId);
                }
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true">
        <MasterTableView DataKeyNames="PostingId">
            <Columns>
                <telerik:GridBoundColumn DataField="JournalGroupName" HeaderStyle-Width="250px"  HeaderText="Group Name"
                    UniqueName="JournalGroupName" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsClosed" HeaderText="Closed"
                    UniqueName="IsClosed" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="120px" DataField="Periode"
                    HeaderText="Periode" UniqueName="Periode" AllowSorting="false" />
                <telerik:GridBoundColumn DataField="EditedBy" AllowSorting="false" HeaderText="Edited By"
                    UniqueName="EditedBy" />
                   
                    
                <telerik:GridTemplateColumn UniqueName="openPostingx" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# ((bool)DataBinder.Eval(Container.DataItem, "IsClosed")) ? 
                            (GetUserUnapprovable() ? 
                            string.Format("<a href=\"#\" onclick=\"openPosting('{0}'); return false;\">{1}</a>",
                            DataBinder.Eval(Container.DataItem, "PostingId"),
                            "<img src=\"../../../../Images/Toolbar/lock16.png\" border=\"0\" title=\"Click to open\" />"):
                            "<img src=\"../../../../Images/Toolbar/lock16_d.png\" border=\"0\" />") : ""%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>    
                    
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateEdited" HeaderText="Edited Date"
                    UniqueName="DateEdited" AllowSorting="false" DataFormatString="dd/MM/yyyy" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
