// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Tilemaps;
// using System.Linq;

// [CreateAssetMenu]
// public class NewCustomRuleTile : RuleTile<NewCustomRuleTile.Neighbor> {
//     public bool connection;
//     public TileBase[] tileToConnect;
//     public TileBase[] checkedTiles;
//     public class Neighbor : RuleTile.TilingRule.Neighbor {
//         public const int upperLeft = 1;
//         public const int upperRight = 2;
//         public const int lowerLeft = 3;
//         public const int lowerRight = 4;
//     }

//     public override bool RuleMatch(int neighbor, TileBase tile) {
//         switch (neighbor) {
//             case Neighbor.upperLeft: return checkUpperLeft();
//             case Neighbor.upperRight: return checkUpperRight();
//             case Neighbor.lowerLeft: return checkLowerLeft();
//             case Neighbor.lowerRight: return checkLowerRight();
//         }
//         return base.RuleMatch(neighbor, tile);
//     }    
//     bool checkUpperLeft(TileBase tile) {
//         if (!connection) {
//             return tile == this;
//         }

//     }
//     bool checkUpperRight(TileBase tile) {

//     }
//     bool checkLowerLeft(TileBase tile) {

//     }
//     bool checkLowerRight(TileBase tile) {
        
//     }
// }
