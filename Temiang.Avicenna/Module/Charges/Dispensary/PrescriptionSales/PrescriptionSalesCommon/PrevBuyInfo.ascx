<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrevBuyInfo.ascx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.PrescriptionSalesCommon.PrevBuyInfo" %>
<asp:Repeater ID="repeater" runat="server">
    <ItemTemplate>
        <fieldset>
            <legend><b>Previous Transaction Info</b></legend>
            <table class="RadGrid RadGrid_Default">
                <tr>
                    <th class="rgHeader" style="width: 240px; text-align: center;">Item Name
                    </th>
                    <th class="rgHeader" style="width: 40px; text-align: center;">Qty
                    </th>
                    <th class="rgHeader" style="width: 50px; text-align: center;">Unit
                    </th>
                    <th class="rgHeader" style="width: 80px; text-align: center;">Date(d/m/y)
                    </th>
                    <th class="rgHeader" style="width: 46px; text-align: center;">Day(s)
                    </th>
                </tr>
                <tr style="color: <%# Eval("Color")%>">
                    <td style="text-align: center;">
                        <%# Eval("ItemName")%>-
                    </td>
                    <td style="text-align: center;">
                        <%# Eval("Qty")%>
                    </td>
                    <td style="text-align: center;">
                        <%# Eval("SRItemUnit")%>
                    </td>
                    <td style="text-align: center;">
                        <%# Eval("Date")%>
                    </td>
                    <td style="text-align: center;">
                        <%# Eval("TotalDays")%>
                    </td>
                </tr>
                <tr class="rgAltRow">
                    <td style="text-align: center;" colspan="5">
                       <%# Eval("ConsumeMethod")%>
                    </td>
                </tr>
            </table>
        </fieldset>
    </ItemTemplate>
</asp:Repeater>
