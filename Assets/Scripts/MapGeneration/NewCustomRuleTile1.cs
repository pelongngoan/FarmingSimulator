// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// [CreateAssetMenu]
// public class NewCustomRuleTile1 : RuleTile<NewCustomRuleTile1.Neighbor> {
//     public bool customField;

//     public class Neighbor : RuleTile.TilingRule.Neighbor {
//         public const int upperL = 1;
//         public const int upperR = 2;
//         public const int lowerL = 3;
//         public const int lowerR = 4;
//     }

//     public override bool RuleMatch(int neighbor, TileBase tile) {
//         switch (neighbor) {
//             case Neighbor.upperL: return upperL();
//             case Neighbor.upperR: return upperR();
//             case Neighbor.lowerL: return lowerL();
//             case Neighbor.lowerR: return lowerR();
//         }
//         return base.RuleMatch(neighbor, tile);
//     }
//     TileBase upperL() {
        
//     }
//     TileBase upperR() {
        
//     }
//     TileBase lowerL() {
        
//     }
//     TileBase lowerR() {
        
//     }
// }