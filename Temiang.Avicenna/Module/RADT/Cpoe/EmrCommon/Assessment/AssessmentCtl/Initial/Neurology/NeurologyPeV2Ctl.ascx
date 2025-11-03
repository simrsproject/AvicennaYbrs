<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NeurologyPeV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.NeurologyPeV2Ctl" %>
<%@ Register TagPrefix="uc1" TagName="GcsCtl" Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<uc1:GcsCtl runat="server" ID="gcsCtl" />
<table style="width: 100%;">
</table>
<fieldset>
    <legend>Stimulus Sign Meningeal</legend>
    <table style="width: 100%;">
        <tr>
            <td class="label">Nuchal Rigidity
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optNuchalRigidity" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
        <tr>
            <td class="label">Kernig
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optKernig" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
        <tr>
            <td class="label">Lasgque
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optLasgque" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>Funduscopy</legend>
    <table style="width: 100%;">
        <tr>
            <td class="label">Papiledema
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optPapiledema" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
    </table>
</fieldset>

<table style="width: 100%;">
    <%--    <tr>
        <td class="label">Nervus Cranialis</td>
        <td>
            <asp:RadioButtonList ID="optNervus" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervus" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>--%>
    <tr>
        <td class="label">Nervus Olfaktoris / Olfactory</td>
        <td>
            <asp:RadioButtonList ID="optNervusOlfaktoris" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusOlfaktoris" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Optikus / Optic</td>
        <td>
            <asp:RadioButtonList ID="optNervusOptikus" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusOptikus" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Okulomotoris / Oculomotor</td>
        <td>
            <asp:RadioButtonList ID="optNervusOkulomotoris" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusOkulomotoris" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Troklear / Trochlear</td>
        <td>
            <asp:RadioButtonList ID="optNervusTroklear" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusTroklear" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Trigeminus / Trigeminal</td>
        <td>
            <asp:RadioButtonList ID="optNervusTrigeminus" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusTrigeminus" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Abducens / Abducens</td>
        <td>
            <asp:RadioButtonList ID="optNervusAbducens" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusAbducens" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Fasialis / Facial Nerve</td>
        <td>
            <asp:RadioButtonList ID="optNervusFasialis" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusFasialis" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Vestibukokhlearis / Vestibulocochlear</td>
        <td>
            <asp:RadioButtonList ID="optNervusVestibukokhlearis" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusVestibukokhlearis" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Glossofaringeal / Glossopharyngeal</td>
        <td>
            <asp:RadioButtonList ID="optNervusGlossofaringeal" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusGlossofaringeal" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Vagus / Vagus Nerve</td>
        <td>
            <asp:RadioButtonList ID="optNervusVagus" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusVagus" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Asesoris / Spinal Accessory</td>
        <td>
            <asp:RadioButtonList ID="optNervusAsesoris" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusAsesoris" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
    <tr>
        <td class="label">Nervus Hipoglossus / Hypoglossal</td>
        <td>
            <asp:RadioButtonList ID="optNervusHipoglossus" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNervusHipoglossus" runat="server" Width="100%" />
        </td>
        <td></td>

    </tr>
</table>
<fieldset>
    <legend>Status Motorik</legend>
    <table style="width: 50%;">
        <tr>
            <td class="labelcaption" style="width: 40%">Left</td>
            <td class="labelcaption" style="width: 20%">&nbsp;</td>
            <td class="labelcaption" style="width: 40%">Right</td>



        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="optExtermintasSuperior" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>

            </td>
            <td class="labelcaption">Extermintas Superior</td>
            <td>
                <asp:RadioButtonList ID="optExtermintasSuperior2" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>

            </td>

        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="optExtermintasInterior" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>

            </td>
            <td class="labelcaption">Extermintas Interior</td>
            <td>
                <asp:RadioButtonList ID="optExtermintasInterior2" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>

            </td>

        </tr>
    </table>
</fieldset>

<table style="width: 100%;">
    <tr>
        <td class="label">Refleks Fisiologis</td>
        <td>
            <telerik:RadTextBox ID="txtRefleksFisiologis" runat="server" Width="100%" />
        </td>
    </tr>
    <tr>
        <td class="label">Refleks Patologis</td>
        <td>
            <telerik:RadTextBox ID="txtRefleksPatologis" runat="server" Width="100%" />
        </td>
    </tr>
    <tr>
        <td class="label">Status Otonom
        </td>
        <td>
            <telerik:RadTextBox ID="txtStatusOtonom" runat="server" Width="100%" />
        </td>
    </tr>

</table>
<table style="width: 100%;">
    <tr>
        <td class="label">Physical Exam
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
    <tr>
        <td class="label">Neurologis
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNeurologis" runat="server" Width="100%" TextMode="MultiLine" Height="100px" Resize="Vertical" />
        </td>
    </tr>

</table>
