<%@ Page Title="Fee Detail" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
CodeBehind="ParamedicFeeRemunDetailDialog.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeRemunDetailDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">

            function openDialogDetailItemRemun(parid, itemid) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                var remunno = '<%=RemunNo %>';

                oWnd.setUrl("ParamedicFeeRemunDetailItemDialog.aspx?parid=" + parid + "&itemid=" + itemid +
                    "&remunno=" + remunno);

                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {

            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="1000px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
   <telerik:RadGrid ID="grdRemunParamedic" runat="server" AutoGenerateColumns="False" GridLines="None"
       OnItemDataBound="grdRemunParamedic_ItemDataBound" >
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParamedicID, ItemID, ServiceUnitID, IdiCode" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ParamedicID" HeaderText="Paramedic ID"
                            UniqueName="ParamedicID" SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic Name" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridTemplateColumn UniqueName="ItemName" HeaderText="Item Name" 
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# string.Format("<a href='#' onclick='javascript:openDialogDetailItemRemun(\"{0}\",\"{1}\")'>{2}</a>",                              
                                    DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                    DataBinder.Eval(Container.DataItem, "ItemID"),
                                    DataBinder.Eval(Container.DataItem, "ItemName"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="IdiCode" HeaderText="Idi Code"
                            UniqueName="IdiCode" SortExpression="IdiCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="IdiName" HeaderText="IDI Name" UniqueName="IdiName"
                            SortExpression="IdiName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ServiceUnitID" HeaderText="Service Unit ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />

                        <telerik:GridBoundColumn DataField="Qty" HeaderText="Qty" UniqueName="Qty"
                            SortExpression="Qty" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Score" HeaderText="Score" UniqueName="Score"
                            SortExpression="Score" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Rvu" HeaderText="Rvu" UniqueName="Rvu"
                            SortExpression="Rvu" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="RvuConversion" HeaderText="Rvu Conversion" UniqueName="RvuConversion"
                            SortExpression="RvuConversion" DataFormatString="{0:n6}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Multiplier" HeaderText="Multiplier Factor" UniqueName="Multiplier"
                            SortExpression="Multiplier" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Coefficient" HeaderText="Coefficient" UniqueName="Coefficient"
                            SortExpression="Coefficient" DataFormatString="{0:n6}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
    <br /><br /><br />
</asp:Content>