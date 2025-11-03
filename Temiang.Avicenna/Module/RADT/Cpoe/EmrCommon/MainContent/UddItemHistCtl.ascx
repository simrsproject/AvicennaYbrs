<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UddItemHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MainContent.UddItemHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.EmrIp" %>
<telerik:RadCodeBlock ID="radCodeBlock" runat="server">

    <script type="text/javascript">

        function verifyPrescription(prescNo) {
            if (!confirm("Verify this Prescription No: " + prescNo + ". Continue ?")) return false;
            // Akan dipanggil dari script yg digenerate pada codebehind
            var masterTable = $find("<%= grdUddItem.ClientID %>").get_masterTableView();
            masterTable.fireCommand('Verify', prescNo);
        }


    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdUddItem">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadGrid ID="grdUddItem" runat="server" EnableViewState="False" Height="560px"
    OnNeedDataSource="grdUddItem_NeedDataSource" OnItemCommand="grdUddItem_ItemCommand"
    AutoGenerateColumns="False" GridLines="None">
    <MasterTableView DataKeyNames="RegistrationNo" ShowHeader="false" CommandItemDisplay="Top">
        <CommandItemTemplate>
            <div>
                <div class="l">
                    <%#IsUserAddAble? string.Format("<a href=\"#\" onclick=\"javascript:entryUddItem('{1}'); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Edit UDD</a>", Helper.UrlRoot()):string.Format("<img src=\"{0}/Images/Toolbar/new16_d.png\"  alt=\"New\" />&nbsp;Edit UDD",Helper.UrlRoot(), RegistrationNo)%>
                    &nbsp;&nbsp;
                    <%#string.Format("<a href=\"#\" onclick=\"javascript:directPrescription(); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Direct Prescription</a>", Helper.UrlRoot())%>
                </div>
                <div class="r">
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="refresh" ImageUrl="~/Images/Toolbar/refresh16.png">
                                            <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" alt=""/>&nbsp;Refresh&nbsp;&nbsp;
                    </asp:LinkButton>
                </div>
            </div>
        </CommandItemTemplate>
        <CommandItemStyle Height="29px" />
        <Columns>
            <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Parent No" UniqueName="ParentNo"
                HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsRFlag" HeaderText="R/"
                UniqueName="IsRFlag" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                Visible="False" />
            <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" Visible="false" />
            <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                <ItemTemplate>
                    <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateResultQty" HeaderStyle-Width="50px"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblResultQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemQtyInString") %>' />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateSRItemUnit" HeaderStyle-Width="100px"
                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? 
                                string.Format("{0} @{1} {2}",DataBinder.Eval(Container.DataItem, "EmbalaceLabel"),DataBinder.Eval(Container.DataItem, "DosageQty"),DataBinder.Eval(Container.DataItem, "SRDosageUnit")) : DataBinder.Eval(Container.DataItem, "SRItemUnit") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="SRConsumeMethodName" HeaderStyle-Width="150px"
                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) && DataBinder.Eval(Container.DataItem, "ParentNo") != null && !string.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "ParentNo").ToString())
                        ? string.Empty : string.Format("{0} @{1} {2}", DataBinder.Eval(Container.DataItem, "SRConsumeMethodName"),  DataBinder.Eval(Container.DataItem, "ConsumeQty"),   DataBinder.Eval(Container.DataItem, "SRConsumeUnit")) %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Stop" CommandName="Delete"
                ButtonType="ImageButton" ConfirmText="Delete this row?">
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
            </telerik:GridButtonColumn>
            <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="False">
        <Selecting AllowRowSelect="False" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
</telerik:RadGrid>


