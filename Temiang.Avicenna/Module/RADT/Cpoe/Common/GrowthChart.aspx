<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="GrowthChart.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.Common.GrowthChart" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/jscript">
        function PrintChartArea() {
            var elem = "chartArea";
            var mywindow = window.open('', 'PRINT', 'height=400,width=600');

            mywindow.document.write('<html><head><title>' + document.title + '</title>');
            mywindow.document.write('</head><body >');
            mywindow.document.write('<h1>' + document.title + '</h1>');
            mywindow.document.write(document.getElementById(elem).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;
        }
    </script>
    <table width="100%">
        <tr>
            <td>
                <fieldset width="100%">
                    <table width="100%">
                        <tr>
                            <td class="label" style="width: 40px">Name</td>
                            <td>:&nbsp;<asp:Label runat="server" ID="lblPatientName"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">Sex</td>
                            <td style="width: 20px">:&nbsp;<asp:Label runat="server" ID="lblSex"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">DOB</td>
                            <td style="width: 90px">:&nbsp;<asp:Label runat="server" ID="lblBirthDate"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">Age</td>
                            <td style="width: 100px">:&nbsp;<asp:Label runat="server" ID="lblAge"></asp:Label>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnPrint" Text="Print" OnClientClick="PrintChartArea();return false;" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <div id="chartArea">
        <div style="height: 4px;"></div>
        <asp:Panel runat="server" ID="pnlChart" Width="100%"></asp:Panel>
    </div>
</asp:Content>
