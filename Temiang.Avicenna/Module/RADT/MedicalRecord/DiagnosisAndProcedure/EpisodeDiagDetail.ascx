<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EpisodeDiagDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.EpisodeDiagDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEpisodeDiagnose" runat="server" ValidationGroup="EpisodeDiagnose" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EpisodeDiagnose"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRDiagnoseType" runat="server" Text="Diagnosis Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRDiagnoseType" runat="server" Width="300px" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvDiagnoseType" runat="server" ErrorMessage="Diagnosis Type required."
                            ValidationGroup="EpisodeDiagnose" ControlToValidate="cboSRDiagnoseType" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiagnoseID" runat="server" Text="Diagnosis ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboDiagnoseID" runat="server" Width="300px" AutoPostBack="true"
                            OnSelectedIndexChanged="cboDiagnoseID_SelectedIndexChanged" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboDiagnoseID_ItemDataBound"
                            OnItemsRequested="cboDiagnoseID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "DiagnoseName") %>
                                </b>
                                <br />
                                Diagnosis ID :
                                <%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 50 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvDiagnoseID" runat="server" ErrorMessage="Diagnosis ID required."
                            ValidationGroup="EpisodeDiagnose" ControlToValidate="cboDiagnoseID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiagnosisText" runat="server" Text="Diagnosis Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDiagnosisText" runat="server" Width="300px" MaxLength="4000"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvDiagnosisText" runat="server" ErrorMessage="Diagnosis Text required."
                            ValidationGroup="EpisodeDiagnose" ControlToValidate="txtDiagnosisText" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExternalCauseID" runat="server" Text="External Cause"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboExternalCauseID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboExternalCauseID_ItemDataBound"
                            OnItemsRequested="cboExternalCauseID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "DiagnoseName") %>
                                </b>
                                <br />
                                Diagnosis ID :
                                <%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMorphologyID" runat="server" Text="Morphology"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboMorphologyID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboMorphologyID_ItemDataBound"
                            OnItemsRequested="cboMorphologyID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "MorphologyName")%>
                                </b>
                                <br />
                                Morphology ID :
                                <%# DataBinder.Eval(Container.DataItem, "MorphologyID")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                            OnItemsRequested="cboParamedicID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ParamedicName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                            ValidationGroup="EpisodeDiagnose" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkAcute" runat="server" Text="Acute Disease" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsChronicDisease" runat="server" Text="Chronic Disease" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsOldCase" runat="server" Text="Old Case" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsConfirmed" runat="server" Text="Confirmed" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>

                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EpisodeDiagnose"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EpisodeDiagnose" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
