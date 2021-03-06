﻿using PuzzleImageGenerator.Shared.Helpers;
using PuzzleImageGenerator.Sq1.Painter.Pieces.Corner;
using PuzzleImageGenerator.Sq1.Painter.Pieces.Edge;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleImageGenerator.Sq1.Painter
{
    public class Sq1Image
    {
        Sq1ImageProp Properties;
        public List<Pieces.Piece> Pieces = new List<Painter.Pieces.Piece>();

        public Sq1Image(Sq1ImageConfiguration configs)
        {
            var isCubeShape = CheckIfCubeshape(configs);

            Properties = new Sq1ImageProp(configs, isCubeShape);

            CreatePieces(configs.StickerDefs);
        }

        static bool CheckIfCubeshape(Sq1ImageConfiguration configs)
        {
            var pieceDefsSplit = configs.StickerDefs.Split(';')[0].Split(',');
            var cubeshapeForm = "cecececeecececec";
            for (int i = 0; i < pieceDefsSplit.Length; i++)
                if (pieceDefsSplit[i][0] != cubeshapeForm[i])
                    return false;
            return true;
        }

        public void CreatePieces(string pieceDefs)
        {
            var curPosition = 0;
            foreach (var pieceDef in pieceDefs.Split(','))
            {
                Pieces.Add(GetPiece(pieceDef, curPosition));
                curPosition += pieceDef[0] == 'e' ? 2 : 4; // Each unit represents 15 degrees of rotation
            }
        }

        private Pieces.Piece GetPiece(string pieceDef, int Position)
        {
            if (pieceDef[0] == 'c')
                return new Corner(pieceDef.Substring(1), Position, Properties);
            else if (pieceDef[0] == 'e')
                return new Edge(pieceDef.Substring(1), Position, Properties);
            else
                return null;
        }

        public string GetSvgText()
        {
            var svgText = SvgHelper.GetHeader(Properties);

            foreach (var piece in Pieces)
            {
                switch (Properties.Stage)
                {
                    case "cubeshape":
                        svgText += SvgHelper.GetPolygonText(piece.Stickers[0].Coords, fill: "lightgrey");
                        break;
                    case "eo":
                    case "obl":
                        svgText += SvgHelper.GetPolygonText(piece.Stickers[0].Coords, fill: piece.Stickers[0].Color);
                        break;
                    case "co":
                        if (piece.Type == Painter.Pieces.PieceType.Corner)
                        {
                            svgText += SvgHelper.GetPolygonText(piece.Stickers[0].Coords, fill: piece.Stickers[0].Color);
                        }
                        else
                        {
                            svgText += SvgHelper.GetPolygonText(piece.Stickers[0].Coords, fill: "lightgrey");
                        }
                        break;
                    case "cp":
                        if (piece.Type == Painter.Pieces.PieceType.Corner)
                        {
                            foreach (var sticker in piece.Stickers)
                                svgText += SvgHelper.GetPolygonText(sticker.Coords, fill: sticker.Color);
                        }
                        else
                        {
                            svgText += SvgHelper.GetPolygonText(piece.Stickers[0].Coords, fill: piece.Stickers[0].Color);
                            svgText += SvgHelper.GetPolygonText(piece.Stickers[1].Coords, fill: "lightgrey");
                        }
                        break;
                    default:
                        foreach (var sticker in piece.Stickers)
                            svgText += SvgHelper.GetPolygonText(sticker.Coords, fill: sticker.Color);
                        break;
                }
            }
            svgText += SvgHelper.GetFooter();
            return svgText;
        }
    }
}
