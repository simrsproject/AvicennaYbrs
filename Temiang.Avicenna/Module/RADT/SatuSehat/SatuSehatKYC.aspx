<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="SatuSehatKYC.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.SatuSehatKYC" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function OnClearClick(sender, args) {
            $find("<%= txtName.ClientID %>").set_value('');
            $find("<%= txtPatSSN.ClientID %>").set_value('');
        }

        function verifyData() {
            var patientName = $find("<%= txtName.ClientID %>").get_value();
            var ssn = $find("<%= txtPatSSN.ClientID %>").get_value();

            if (!patientName) {
                alert("Patient Name is required.");
                return false;
            }

            if (!ssn) {
                alert("SSN / NIK is required.");
                return false;
            }

            var ssnRegex = /^\d{16}$/;
            if (!ssnRegex.test(ssn)) {
                alert("SSN / NIK must be exactly 16 digits and numeric only.");
                return false;
            }

            alert("Data verified successfully!");
            document.getElementById("<%= btnOpenNewWindow.ClientID %>").style.display = "inline-block";
        }

        function openNewWindow() {
            var url = document.getElementById('<%= hfKYCUrl.ClientID %>').value;
            if (url) {
                window.open(url, "_blank");
            } else {
                alert("Invalid URL. Please try again.");
            }
        }
    </script>
    <body style="margin: 0; padding: 0; height: 100vh; width: 100vw; overflow: hidden;">
        <div style="display: flex; flex-direction: column; justify-content: center; align-items: center; height: 100vh; text-align: center;">
            <div style="margin-bottom: 30px;">
                <img src="../../../Images/logo-satusehat.png" alt="SatuSehat Logo" style="width: 200px; height: auto;" />
            </div>
            <table>
                <tr>
                    <td colspan="2" style="text-align: left; font-size: 18px; font-weight: bold; padding-bottom: 5px;">Operator Name</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtName" runat="server" Width="400px" Height="40px" Font-Size="16px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Patient Name Required."
                            ValidationGroup="entry" ControlToValidate="txtName" SetFocusOnError="True">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 15px;"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left; font-size: 18px; font-weight: bold; padding-bottom: 5px;">SSN / NIK</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtPatSSN" runat="server" Width="400px" Height="40px" Font-Size="16px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:RequiredFieldValidator ID="rfvSSN" runat="server" ErrorMessage="SSN / NIK Required."
                            ValidationGroup="entry" ControlToValidate="txtPatSSN" SetFocusOnError="True">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 20px;"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnVerify" runat="server" Text="Verify Data" CssClass="btn" Font-Size="16px" Width="200px"
                            OnClick="btnVerify_Click" ValidationGroup="entry" />
                        &nbsp;&nbsp;
                        <asp:HiddenField ID="hfKYCUrl" runat="server" />
                        <asp:Button ID="btnOpenNewWindow" runat="server" Text="Process To KYC" CssClass="btn" Font-Size="16px" Width="200px"
                            Style="display: none;" OnClientClick="openNewWindow(); return false;" />
                    </td>
                </tr>
            </table>
        </div>
    </body>
</asp:Content>
