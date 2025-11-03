<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionReviewItemPicker.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.PrescriptionReviewItemPicker" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <link rel="stylesheet" href="<%=Helper.UrlRoot()%>/App_Themes/Default/SmallSwitch.css">
        <script type="text/javascript" language="javascript">
            function GetSelectedNames() {
                var grid = $find("<%= grdPrescriptionItem.ClientID %>");
                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var selectedRows = masterTable.get_selectedItems();
                    var retval = "";
                    for (var i = 0; i < selectedRows.length; i++) {
                        var row = selectedRows[i];
                        var txtInformation = $telerik.findControl(row.get_element(), "txtInformation")
                        retval = retval + "\n" + row.get_cell("ItemName").innerHTML + ": " + txtInformation.get_value();

                        //// Method1 
                        //var getCellText_1 = row.get_element().cells[0].innerHTML;
                        //// Method2 
                        //var getCellText_2 = row.get_cell("ReplacementID").innerHTML;
                        //// Method3 
                        //var getCellText_2 = row.get_cell("ReplacementID").getElementsByTagName("span")[0].innerHTML; //this code also work for Checkboxcolunm, hyperlinkcolumn...etc 
                    }
                    alert(retval)

                }
            }
        </script>
    </telerik:RadCodeBlock>
    <input onclick="GetSelectedNames();" type="button" value="Get selected rows contact names">--%>
    <telerik:RadGrid ID="grdPrescriptionItem" runat="server" OnNeedDataSource="grdPrescriptionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="false" AllowMultiRowSelection="true">
        <MasterTableView DataKeyNames="SequenceNo">
            <Columns>
                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn"></telerik:GridClientSelectColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="No" UniqueName="SequenceNo"
                    HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                    UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Hd" UniqueName="ParentNo"
                    HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" Display="false" />
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateItemName3" HeaderStyle-Width="60px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PrescriptionQty") %><br />
                        (<%# DataBinder.Eval(Container.DataItem, "ResultQty") %>)
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Dosing"
                    UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                    UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method" HeaderStyle-Width="100px"
                    UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" />
                <telerik:GridTemplateColumn HeaderText="Information" UniqueName="Information">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtInformation" runat="server"
                            Width="100%">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>


        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>


</asp:Content>
