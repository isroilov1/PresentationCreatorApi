F R O M   m c r . m i c r o s o f t . c o m / d o t n e t / s d k : 8 . 0   A S   b u i l d 
 
 W O R K D I R   / a p p 
 
 E X P O S E   8 0 
 
 
 
 F R O M   m c r . m i c r o s o f t . c o m / d o t n e t / s d k : 8 . 0   A S   b u i l d 
 
 W O R K D I R   / s r c 
 
 C O P Y   [ " P r e s e n t a t i o n C r e a t o r A P I / P r e s e n t a t i o n C r e a t o r A P I . c s p r o j " ,   " P r e s e n t a t i o n C r e a t o r A P I / " ] 
 
 C O P Y   [ " P r e s e n t a t i o n C r e a t o r A P I . D o m a i n / P r e s e n t a t i o n C r e a t o r A P I . D o m a i n . c s p r o j " ,   " P r e s e n t a t i o n C r e a t o r A P I . D o m a i n / " ] 
 
 C O P Y   [ " P r e s e n t a t i o n C r e a t o r A P I . D a t a / P r e s e n t a t i o n C r e a t o r A P I . D a t a . c s p r o j " ,   " P r e s e n t a t i o n C r e a t o r A P I . D a t a / " ] 
 
 C O P Y   [ " P r e s e n t a t i o n C r e a t o r A P I . A p p l i c a t i o n / P r e s e n t a t i o n C r e a t o r A P I . A p p l i c a t i o n . c s p r o j " ,   " P r e s e n t a t i o n C r e a t o r A P I . A p p l i c a t i o n / " ] 
 
 R U N   d o t n e t   r e s t o r e   " P r e s e n t a t i o n C r e a t o r A P I / P r e s e n t a t i o n C r e a t o r A P I . c s p r o j " 
 
 C O P Y   .   . 
 
 W O R K D I R   " / s r c / P r e s e n t a t i o n C r e a t o r A P I " 
 
 R U N   d o t n e t   b u i l d   " P r e s e n t a t i o n C r e a t o r A P I . c s p r o j "   - c   R e l e a s e   - o   / a p p / b u i l d 
 
 
 
 F R O M   b u i l d   A S   p u b l i s h 
 
 R U N   d o t n e t   p u b l i s h   " P r e s e n t a t i o n C r e a t o r A P I . c s p r o j "   - c   R e l e a s e   - o   / a p p / p u b l i s h 
 
 
 
 F R O M   m c r . m i c r o s o f t . c o m / d o t n e t / a s p n e t : 8 . 0   A S   r u n t i m e 
 
 W O R K D I R   / a p p 
 
 C O P Y   - - f r o m = b u i l d   / a p p / P r e s e n t a t i o n C r e a t o r A P I / o u t   . 
 
 E N T R Y P O I N T   [ " d o t n e t " ,   " P r e s e n t a t i o n C r e a t o r A P I . d l l " ] 