<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IgdPeV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.IgdPeV2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>PRIMARY SURVEY</legend>
    <table style="width: 50%">
        <tr>
            <td class="label">ATS</td>
            <td>
                <table width="100%">
                    <tr>
                        <td class="label">ATS No</td>
                        <td>
                            <telerik:RadDropDownList ID="ddlEsi" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlEsi_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Condition</td>
                        <td>
                            <telerik:RadCheckBoxList ID="cblEsiCondition" runat="server" Width="100%" AutoPostBack="False" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
    <fieldset style="width: 98%;">
        <legend>JALAN NAFAS</legend>
        <table style="width: 55%">
            <tr>
                <td class="label">Paten</td>
                <td>
                    <asp:RadioButtonList ID="optPaten" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtPaten" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Obstruksi Partial</td>
                <td>
                    <asp:RadioButtonList ID="optObsPartial" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtObsPartial" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Obstruksi total</td>
                <td>
                    <asp:RadioButtonList ID="optObsTotal" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtObsTotal" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Trauma jalan napas</td>
                <td>
                    <asp:RadioButtonList ID="optTrauma" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtTrauma" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Resiko aspirasi</td>
                <td>
                    <asp:RadioButtonList ID="optResiko" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtResiko" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Benda asing</td>
                <td>
                    <asp:RadioButtonList ID="optBendaAsing" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtBendaAsing" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Kesimpulan</td>
                <td>
                    <asp:RadioButtonList ID="optKesimpulan" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtKesimpulan" runat="server" Width="100%" />
                </td>
            </tr>
        </table>
    </fieldset>
    <table style="width: 100%">
        <tr>
            <td valign="top" style="width: 50%">
                <%--                <uc1:QuestionCtl runat="server" ID="questJalanNapas" IsUseQuestionGroupCaption="True" />--%>
                <uc1:QuestionCtl runat="server" ID="questPernapasan" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questSirkulasi" IsUseQuestionGroupCaption="True" />
            </td>
            <td valign="top" style="width: 50%">
                <uc1:QuestionCtl runat="server" ID="questPenilaianBayi" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questDisabilitas" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questEksposur" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset style="width: 98%;">
    <legend>SECONDARY SURVEY</legend>
    <table style="width: 100%">
        <tr>
            <td style="width: 50%" valign="top">
                <uc1:GcsCtl runat="server" ID="gcsCtl" />
            </td>
        </tr>
    </table>
    <fieldset style="width: 98%;">
        <legend>KEPALA LEHER</legend>
        <table style="width: 55%">
            <tr>
                <td class="label">KEPALA</td>
                <td>
                    <asp:RadioButtonList ID="optKepala" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtKepala" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Konjungtiva</td>
                <td>
                    <asp:RadioButtonList ID="optKonjungtiva" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtKonjungtiva" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Sklera</td>
                <td>
                    <asp:RadioButtonList ID="optSklera" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtSklera" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Bibir / Lidah</td>
                <td>
                    <asp:RadioButtonList ID="optBibirLidah" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtBibirLidah" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Mukosa</td>
                <td>
                    <asp:RadioButtonList ID="optMukosa" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtMukosa" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Mata</td>
                <td>
                    <asp:RadioButtonList ID="optMata" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtMata" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Kondisi kepala / kondisi lainnya</td>
                <td>
                    <asp:RadioButtonList ID="optKondisiKepala" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtKondisiKepala" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">LEHER</td>
                <td>
                    <asp:RadioButtonList ID="optLeher" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtLeher" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Deviasi Trakea</td>
                <td>
                    <asp:RadioButtonList ID="optTrakea" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtTrakea" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">JVP</td>
                <td>
                    <asp:RadioButtonList ID="optJvp" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtJvp" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">LNN</td>
                <td>
                    <asp:RadioButtonList ID="optLNN" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtLNN" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Tiroid</td>
                <td>
                    <asp:RadioButtonList ID="optTiroid" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtTiroid" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Kondisi Leher</td>
                <td>
                    <asp:RadioButtonList ID="optKondisiLeher" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtKondisiLeher" runat="server" Width="100%" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 98%;">
        <legend>THORAX</legend>
        <table style="width: 55%">
            <tr>
                <td class="label">Thorax</td>
                <td>
                    <asp:RadioButtonList ID="optThorax" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtThorax" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Jantung</td>
                <td>
                    <asp:RadioButtonList ID="optJantung" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtJantung" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Paru</td>
                <td>
                    <asp:RadioButtonList ID="optParu" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtParu" runat="server" Width="100%" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 98%;">
        <legend>ABDOMEN & PELVIS</legend>
        <table style="width: 55%">
            <tr>
                <td class="label">Abdomen</td>
                <td>
                    <asp:RadioButtonList ID="optAbdomen" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtAbdomen" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Punggung & Pinggang</td>
                <td>
                    <asp:RadioButtonList ID="optPunggung" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtPunggung" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Ekstremitas</td>
                <td>
                    <asp:RadioButtonList ID="optEkstremitas" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtEkstremitas" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Genitalia</td>
                <td>
                    <asp:RadioButtonList ID="optGenitalia" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtGenitalia" runat="server" Width="100%" />
                </td>
            </tr>
        </table>
    </fieldset>
</fieldset>
<fieldset style="width: 48%;">
    <legend>ANCILLARY EXAM</legend>
    <uc1:QuestionCtl runat="server" ID="questAncillaryExam" IsUseQuestionGroupCaption="True" />
</fieldset>
