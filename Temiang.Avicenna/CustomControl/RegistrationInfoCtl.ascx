<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationInfoCtl.ascx.cs"
    Inherits="Temiang.Avicenna.CustomControl.RegistrationInfoCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<table width="100%">
    <tr>
        <td valign="top" width="450px">
            <fieldset>
                <div style="font-weight: bold; font-size: large; color: yellow; background-color: black; text-align: center;">
                    <asp:Label runat="server" ID="lblPatientName" />
                </div>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: top; width: 125px">
                            <fieldset style="min-height: 125px;">
                                <div>
                                    <asp:Image runat="server" ID="imgPatientPhoto" Width="120px" Height="120px" />
                                </div>
                            </fieldset>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td width="50px">MRN
                                    </td>
                                    <td width="4px">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblMedicalNo" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Reg. No
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblRegistrationNo" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Reg. Date
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblRegistrationDate" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Gender
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblGender" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">DOB
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDateOfBirth" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Guarantor
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblGuarantor" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Unit
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblServiceUnit" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Room
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblRoom" Font-Bold="true" />
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>

                </table>
            </fieldset>            
            <fieldset runat="server" id="divChronicDisease">
                <legend style="background-color: red; text-align: center; color: white; font-weight: bold">Chronic Disease</legend>
                <asp:Label runat="server" ID="lblChronicDisease" Width="100%" Font-Size="X-Large" />
            </fieldset>
            <asp:HiddenField runat="server" ID="hdnIsShowPatientAllergy" Value="1" />
            <fieldset runat="server" id="pnlPatientAllergy">
                <legend><strong>Allergies</strong></legend>
                <asp:Literal runat="server" ID="litPatientAllergy"></asp:Literal>
            </fieldset>            
            <div style="height: 4px">
            </div>
            <asp:HiddenField runat="server" ID="hdnIsShowVitalSign" Value="1" />
            <fieldset runat="server" id="pnlVitalSign">
                <legend><strong>Last Vital Sign</strong></legend>
                <asp:Literal runat="server" ID="litVitalSign"></asp:Literal>
            </fieldset>
        </td>
        <td valign="top">
            <asp:HiddenField runat="server" ID="hdnIsShowDiagnosis" Value="1" />
            <fieldset runat="server" id="pnlDiagnosis" style="width: 450px">
                <legend><strong>Diagnosis</strong></legend>
                <asp:Literal runat="server" ID="litDiagnosis"></asp:Literal>
            </fieldset>

            <div style="height: 4px">
            </div>
            <asp:HiddenField runat="server" ID="hdnIsShowPhysicianTeam" Value="1" />
            <fieldset runat="server" id="pnlPhysicianTeam" style="width: 450px">
                <legend><strong>Physician Team</strong></legend>
                <asp:Literal runat="server" ID="litPhysicianTeam"></asp:Literal>
            </fieldset>
        </td>
        <td></td>
    </tr>
</table>

