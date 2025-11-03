<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IgdEsiOptPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.IgdEsiOptPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<telerik:RadCodeBlock runat="server">
    <script type="text/javascript">
        (function (global, undefined) {
            function toggleShow(elId, val) {
                var el = document.getElementById(elId);
                if (val === 1) {
                    el.style.display = "block";
                } else {
                    el.style.display = "none";
                }
            }
            global.toggleShow = toggleShow;

        })(window);

    </script>
</telerik:RadCodeBlock>

<telerik:RadAjaxPanel runat="server" ID="ajxPnl">
    <fieldset style="width: 49%;">
        <legend><strong>Triage</strong></legend>

        <table width="100%">
            <tr id="trPainScale" runat="server">
                <td class="label">Pain Scale</td>
                <td>
                    <asp:RadioButtonList ID="optPainScale" runat="server" RepeatDirection="Horizontal" Width="400px">
                        <asp:ListItem Text="0" Value="0">0<img class="ps00"/></asp:ListItem>
                        <asp:ListItem Text="1" Value="1">1<img class="ps01"/></asp:ListItem>
                        <asp:ListItem Text="2" Value="2">2<img class="ps02"/></asp:ListItem>
                        <asp:ListItem Text="3" Value="3">3<img class="ps03"/></asp:ListItem>
                        <asp:ListItem Text="4" Value="4">4<img class="ps04"/></asp:ListItem>
                        <asp:ListItem Text="5" Value="5">5<img class="ps05"/></asp:ListItem>
                        <asp:ListItem Text="6" Value="6">6<img class="ps06"/></asp:ListItem>
                        <asp:ListItem Text="7" Value="7">7<img class="ps07"/></asp:ListItem>
                        <asp:ListItem Text="8" Value="8">8<img class="ps08"/></asp:ListItem>
                        <asp:ListItem Text="9" Value="9">9<img class="ps09"/></asp:ListItem>
                        <asp:ListItem Text="10" Value="10">10<img class="ps10"/></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
            </tr>
            <tr id="trFlacc" runat="server">
                <td class="label">FLACC</td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="label">Face</td>
                            <td>
                                <telerik:RadDropDownList ID="ddlFlaccFace" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Legs</td>
                            <td>
                                <telerik:RadDropDownList ID="ddlFlaccLegs" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Activity</td>
                            <td>
                                <telerik:RadDropDownList ID="ddlFlaccActivity" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Cry</td>
                            <td>
                                <telerik:RadDropDownList ID="ddlFlaccCry" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Consolability</td>
                            <td>
                                <telerik:RadDropDownList ID="ddlFlaccConsolability" runat="server" Width="100%" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">ESI</td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="label">ESI No</td>
                            <td>
                                <telerik:RadDropDownList ID="ddlEsi" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlEsi_SelectedIndexChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Condition</td>
                            <td>
                                <telerik:RadCheckBoxList ID="cblEsiCondition" runat="server" Width="100%"  AutoPostBack="False" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
        </table>
    </fieldset>
</telerik:RadAjaxPanel>
<fieldset style="width: 800px;">
    <legend><strong>Subjective</strong></legend>

    <table width="100%">
        <tr>
            <td class="label"></td>
            <td>
                <telerik:RadTextBox ID="txtSubjective" runat="server" Width="100%" Height="100px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
    </table>
</fieldset>
<fieldset style="width: 800px;">
    <legend><strong>Objective</strong></legend>

    <table width="100%">
        <tr>
            <td class="label">Head</td>
            <td style="width: 150px;">
                <telerik:RadRadioButtonList ID="optHead" runat="server" Layout="Flow"  Direction="Horizontal" AutoPostBack="False">
                    <Items>
                        <telerik:ButtonListItem Text="Normal" Value="N" Selected="true" />
                        <telerik:ButtonListItem Text="Abnormal" Value="A" />
                    </Items>
                    <%--<ClientEvents OnSelectedIndexChanged="optHead_IndexChanged" />--%>
                </telerik:RadRadioButtonList>
                <%--<telerik:RadCodeBlock runat="server">
                    <script type="text/javascript">
                        (function (global, undefined) {
                            function optHead_IndexChanged(list, args) {
                                toggleShow("<%=txtHead.ClientID%>", args.get_newSelectedIndex());
                            }
                            global.optHead_IndexChanged = optHead_IndexChanged;
                        })(window);
                    </script>
                </telerik:RadCodeBlock>--%>

            </td>
            <td>
                <telerik:RadTextBox ID="txtHead" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
    <td class="label" style="vertical-align: top;">Eyes</td>
        <td style="vertical-align: top;">
            <telerik:RadRadioButtonList runat="server" ID="optEye" AutoPostBack="False" Layout="Flow"  Direction="Horizontal">
                <Items>
                    <telerik:ButtonListItem Text="Normal" Value="N" Selected="true" />
                    <telerik:ButtonListItem Text="Abnormal" Value="A" />
                </Items>
                <ClientEvents OnSelectedIndexChanged="optEye_IndexChanged" />
            </telerik:RadRadioButtonList>
            <telerik:RadCodeBlock runat="server">
                <script type="text/javascript">
                    (function (global, undefined) {
                        function optEye_IndexChanged(list, args) {
                            toggleShow("tblEyeAbn", args.get_newSelectedIndex());
                        }
                        global.optEye_IndexChanged = optEye_IndexChanged;
                    })(window);

                </script>
            </telerik:RadCodeBlock>
        </td>
        <td style="padding-top: 30px;">
            <table style="width: 100%" id="tblEyeAbn">
                <tr>
                    <td style="width: 100px;">
                        <telerik:RadCheckBox runat="server" ID="chkEyesAnemia" Text="Anemia"  AutoPostBack="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadCheckBox runat="server" ID="chkEyesIkterik" Text="Ikterik"  AutoPostBack="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadCheckBox runat="server" ID="chkEyesPupil" Text="Pupil Anisokor"  AutoPostBack="False" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEyesPupil" runat="server" Width="100%" /></td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadCheckBox runat="server" ID="chkEyesOther" Text="Other"  AutoPostBack="False" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEyesOther" runat="server" Width="100%" /></td>
                </tr>
            </table>
        </td>
        <td></td>
    </tr>

        <tr>
            <td class="label" style="vertical-align: top;">Neck</td>
            <td>
                <telerik:RadRadioButtonList runat="server" ID="optNeck" AutoPostBack="False" Layout="Flow"  Direction="Horizontal">
                    <Items>
                        <telerik:ButtonListItem Text="Normal" Value="N"  />
                        <telerik:ButtonListItem Text="Abnormal" Value="A" />
                    </Items>
                    <%--<ClientEvents OnSelectedIndexChanged="optNeck_IndexChanged" />--%>
                </telerik:RadRadioButtonList>
                <%--<telerik:RadCodeBlock runat="server">
                    <script type="text/javascript">
                        (function (global, undefined) {
                            function optNeck_IndexChanged(list, args) {
                                toggleShow("<%=txtNeck.ClientID%>", args.get_newSelectedIndex());
                            }
                            global.optNeck_IndexChanged = optNeck_IndexChanged;
                        })(window);
                    </script>
                </telerik:RadCodeBlock>--%>
            </td>
            <td>
                <telerik:RadTextBox ID="txtNeck" runat="server" Width="100%" />
            </td>
        </tr>

        <tr>
            <td class="label" style="vertical-align: top;">Pulmo</td>
            <td style="vertical-align: top;">
                <telerik:RadRadioButtonList runat="server" ID="optPul" AutoPostBack="False" Layout="Flow"  Direction="Horizontal">
                    <Items>
                        <telerik:ButtonListItem Text="Normal" Value="N" />
                        <telerik:ButtonListItem Text="Abnormal" Value="A" />
                    </Items>
                    <ClientEvents OnSelectedIndexChanged="optPul_IndexChanged" />
                </telerik:RadRadioButtonList>
                <telerik:RadCodeBlock runat="server">
                    <script type="text/javascript">
                        (function (global, undefined) {
                            function optPul_IndexChanged(list, args) {
                                toggleShow("tblPulmoAbn", args.get_newSelectedIndex());
                            }
                            global.optPul_IndexChanged = optPul_IndexChanged;
                        })(window);
                    </script>
                </telerik:RadCodeBlock>
            </td>
            <td style="padding-top: 30px;">
                <table style="width: 100%" id="tblPulmoAbn">
                    <tr>
                        <td style="width: 100px;">
                            <telerik:RadCheckBox runat="server" ID="chkPulRonki" Text="Ronki"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkPulWheezing" Text="Wheezing"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkPulRetrac" Text="Retraction"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkPulOther" Text="Other"  AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPulOther" runat="server" Width="100%" /></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="label" style="vertical-align: top;">COR</td>
            <td style="vertical-align: top;">
                <telerik:RadRadioButtonList runat="server" ID="optCor" AutoPostBack="False" Layout="Flow" Direction="Horizontal">
                    <Items>
                        <telerik:ButtonListItem Text="Normal" Value="N" />
                        <telerik:ButtonListItem Text="Abnormal" Value="A" />
                    </Items>
                    <ClientEvents OnSelectedIndexChanged="optCor_IndexChanged" />
                </telerik:RadRadioButtonList>
                <telerik:RadCodeBlock runat="server">
                    <script type="text/javascript">
                        (function (global, undefined) {
                            function optCor_IndexChanged(list, args) {
                                toggleShow("tblCorAbn", args.get_newSelectedIndex());
                            }
                            global.optCor_IndexChanged = optCor_IndexChanged;
                        })(window);
                    </script>
                </telerik:RadCodeBlock>
            </td>
            <td style="padding-top: 30px;">
                <table style="width: 100%" id="tblCorAbn">
                    <tr>
                        <td style="width: 100px;">
                            <telerik:RadCheckBox runat="server" ID="chkCorGallop" Text="Gallop"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkCorMurmur" Text="Murmur"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkCorOther" Text="Other"  AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtCorOther" runat="server" Width="100%" /></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="label" style="vertical-align: top;">Abdomen</td>
            <td style="vertical-align: top;">
                <telerik:RadRadioButtonList runat="server" ID="optAbd" AutoPostBack="False" Layout="Flow" Direction="Horizontal">
                    <Items>
                        <telerik:ButtonListItem Text="Normal" Value="N" />
                        <telerik:ButtonListItem Text="Abnormal" Value="A" />
                    </Items>
                    <ClientEvents OnSelectedIndexChanged="optAbd_IndexChanged" />
                </telerik:RadRadioButtonList>
                <telerik:RadCodeBlock runat="server">
                    <script type="text/javascript">
                        (function (global, undefined) {
                            function optAbd_IndexChanged(list, args) {
                                toggleShow("tblAbdAbn", args.get_newSelectedIndex());
                            }
                            global.optAbd_IndexChanged = optAbd_IndexChanged;
                        })(window);
                    </script>
                </telerik:RadCodeBlock>
            </td>
            <td style="padding-top: 30px;">
                <table style="width: 100%" id="tblAbdAbn">
                    <tr>
                        <td style="width: 150px;">
                            <telerik:RadCheckBox runat="server" ID="chkAbdBiSusUp" Text="Bowel sounds increased"  AutoPostBack="False" DisabledCssClass="" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkAbdBiSusDown" Text="Decreased bowel sounds"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkAbdPressPain" Text="Pressure Pain"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkAbdHepa" Text="Hepatomegali"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkAbdSple" Text="Splenomegali"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkAbdRelPain" Text="Release Pain"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkAbdOther" Text="Other"  AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtAbdOther" runat="server" Width="100%" /></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="label" style="vertical-align: top;">Extremity</td>
            <td style="vertical-align: top;">
                <telerik:RadRadioButtonList runat="server" ID="optExt" AutoPostBack="False" Layout="Flow"  Direction="Horizontal">
                    <Items>
                        <telerik:ButtonListItem Text="Normal" Value="N" />
                        <telerik:ButtonListItem Text="Abnormal" Value="A" />
                    </Items>
                    <ClientEvents OnSelectedIndexChanged="optExt_IndexChanged" />
                </telerik:RadRadioButtonList>
                <telerik:RadCodeBlock runat="server">
                    <script type="text/javascript">
                        (function (global, undefined) {
                            function optExt_IndexChanged(list, args) {
                                toggleShow("tblExtAbn", args.get_newSelectedIndex());
                            }
                            global.optExt_IndexChanged = optExt_IndexChanged;
                        })(window);
                    </script>
                </telerik:RadCodeBlock>
            </td>
            <td style="padding-top: 30px;">
                <table style="width: 100%" id="tblExtAbn">
                    <tr>
                        <td style="width: 150px;">
                            <telerik:RadCheckBox runat="server" ID="chkExtColdAkral" Text="Cold Akral" AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkExtPitEdem" Text="Pitting Edema"  AutoPostBack="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkExtWeakPul" Text="Weak pulse"  AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkExtPares" Text="Paresis"  AutoPostBack="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkExtCrt2up" Text="CRT >2 seconds"  AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkExtMenSs" Text="Meningeal Stimulation Sign"  AutoPostBack="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkExtParesthe" Text="Paresthesia"  AutoPostBack="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkExtArmMus" Text="Arm Muscle Strength"  AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtExtArmMus" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="chkExtLegMus" Text="Leg Muscle Strength"  AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtExtLegMus" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadCheckBox runat="server" ID="optExtOther" Text="Other"  AutoPostBack="False" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtExtOther" runat="server" Width="100%" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">Skin</td>
            <td>
                <telerik:RadRadioButtonList runat="server" ID="optSkin" AutoPostBack="False" Layout="Flow"  Direction="Horizontal">
                    <Items>
                        <telerik:ButtonListItem Text="Normal" Value="N" />
                        <telerik:ButtonListItem Text="Abnormal" Value="A" />
                    </Items>
                    <%--<ClientEvents OnSelectedIndexChanged="optSkin_IndexChanged" />--%>
                </telerik:RadRadioButtonList>
                <%--<telerik:RadCodeBlock runat="server">
                    <script type="text/javascript">
                        (function (global, undefined) {
                            function optSkin_IndexChanged(list, args) {
                                toggleShow("<%=txtSkin.ClientID%>", args.get_newSelectedIndex());
                            }
                            global.optSkin_IndexChanged = optSkin_IndexChanged;
                        })(window);
                    </script>
                </telerik:RadCodeBlock>--%>
            </td>
            <td>
                <telerik:RadTextBox ID="txtSkin" runat="server" Width="100%" />
            </td>

        </tr>
        <tr id="trNutriSkrinning" runat="server" visible="false">
            <td class="label">Nutrition Skrinning</td>
            <td colspan="2">
                <asp:RadioButtonList ID="optNutritionSkrinning" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                    <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                    <asp:ListItem Text="Malnutrition" Value="Malnutrition"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="label">Other
            </td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtObjectiveNotes" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>

        </tr>
    </table>
</fieldset>
<telerik:RadCodeBlock runat="server">
    <script type="text/javascript">
        function ShowAbnormalState() {
            <%--toggleShow("<%=txtHead.ClientID%>", <%=optHead.SelectedIndex%>);--%>
            toggleShow("tblEyeAbn", <%=optEye.SelectedIndex%>);
            <%--toggleShow("<%=txtNeck.ClientID%>", <%=optNeck.SelectedIndex%>);--%>
            toggleShow("tblPulmoAbn", <%=optPul.SelectedIndex%>);
            toggleShow("tblCorAbn", <%=optCor.SelectedIndex%>);
            toggleShow("tblAbdAbn", <%=optAbd.SelectedIndex%>);
            toggleShow("tblExtAbn", <%=optExt.SelectedIndex%>);
            <%--toggleShow("<%=txtSkin.ClientID%>", <%=optSkin.SelectedIndex%>);--%>

            Sys.Application.remove_load(ShowAbnormalState);
        }

        Sys.Application.add_load(ShowAbnormalState);

    </script>
</telerik:RadCodeBlock>
