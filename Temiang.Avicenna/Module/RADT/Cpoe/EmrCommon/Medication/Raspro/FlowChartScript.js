(function(global, undefined) {
    var diagram;
 
    function pageLoad() {
        capAllConnections(diagram.connections);
        cleanUpShapesContent(diagram.shapes);
    }
 
    function diagram_load(sender) {
        diagram = sender.get_kendoWidget();
    }
 
    function visualizeShape(options) {
        var ns = kendo.dataviz.diagram,
            diagramCanvas = getDiagramCanvasOnPage(),
            lineHeight = 16,
            type = options.type,
            shapeGroup = new ns.Group({ autoSize: true }),
            textGroup = new ns.Group(),
            textLines = [];
 
        if(options.type != "text" && options.content && options.content.text) {
            text = options.content.text.split("\\n");
 
            var textHeight = options.height - (text.length - 1) * lineHeight;
 
            for(var i = 0; i < text.length; i++) {
                var y = (i * lineHeight);
 
                textLines.push(new ns.TextBlock({
                    autoSize: false,
                    text: text[i],
                    x: 0,
                    y: y,
                    width: options.width,
                    height: textHeight + 2*y,
                    color: options.stroke.color,
                    fontFamily: "Segoe UI"
                }));
            }
            options.content.text = "";
        }
 
        if(type == "rectangle") {
            var rectangle = new ns.Rectangle(options);
            appendToGroupWithoutOffset(rectangle, shapeGroup);
        }
        else if(type == "question") {
            options.data = "M70,0 L140,70 L70,140 L0,70 z";
            var path = new ns.Path(options);
            appendToGroupWithoutOffset(path, shapeGroup);
        }
        else if(type == "start" || type == "end") {
            var circle = new ns.Circle(options);
            appendToGroupWithoutOffset(circle, shapeGroup);
        }
         
        if(options.type != "text") {
            diagramCanvas.append(shapeGroup);
            var lineHeight_x2 = 2 * lineHeight,
                box = shapeGroup.drawingElement.bbox(),
                largestTextContainerHeight = box.size.height + lineHeight * (textLines.length - 1);
 
            for(var j = textLines.length - 1, textEdge = largestTextContainerHeight; j >= 0; j--, textEdge -= lineHeight_x2) {
                var textLine = textLines[j];
                shapeGroup.append(textLine);
                var containerRect = new kendo.dataviz.diagram.Rect(box.origin.x, box.origin.y, box.size.width, textEdge);
                alignTextShape(containerRect, textLine);
            }
            diagramCanvas.remove(shapeGroup);
        }
 
        return shapeGroup;
    }
 
    function appendToGroupWithoutOffset(shape, group) {
        shape.position(0, 0);
        group.append(shape);
    }
 
    function alignTextShape(containerRect, textLine) {
        var aligner = new kendo.dataviz.diagram.RectAlign(containerRect);
        var contentBounds = textLine.drawingElement.bbox(null);
 
        var contentRect = new kendo.dataviz.diagram.Rect(0, 0, contentBounds.width(), contentBounds.height());
        var alignedBounds = aligner.align(contentRect, "center middle");
 
        textLine.position(alignedBounds.topLeft());
    }
 
    function createCustomMarker() {
        return new kendo.dataviz.diagram.ArrowMarker({
            path: "M 0 0 L 8 4 L 0 8 L 2 4 z",
            fill: "#6c6c6c",
            stroke: {
                color: "#6c6c6c",
                width: 0.5
            },
            id: "custom",
            orientation: "auto",
            width: 10,
            height: 10,
            anchor: new kendo.dataviz.diagram.Point(7, 4)
        });
    }
 
    function capAllConnections(connections) {
        Array.forEach(connections, function(connection) {
            var marker = createCustomMarker();
            connection.path._markers.end = marker;
            connection.path.drawingContainer().append(marker.drawingElement);
            connection.path._redrawMarkers(true, connection.path.options);
        });
    }
    function cleanUpShapesContent(shapes) {
        Array.forEach(shapes, function(shape) {
            if(!/Ya|Tidak/.test(shape.content())) {
                shape.visual.remove(shape._contentVisual);
            }
        });
    }
 
    function getDiagramCanvasOnPage() {
        return $telerik.$(".k-diagram").getKendoDiagram().canvas;
    }
 
    global.pageLoad = pageLoad;
    global.diagram_load = diagram_load;
    global.visualizeShape = visualizeShape;
})(window);