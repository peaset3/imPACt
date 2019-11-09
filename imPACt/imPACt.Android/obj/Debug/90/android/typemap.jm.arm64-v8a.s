	/* Data SHA1: 9b2df6ade2cffe8dfc713f75baae6da2d95c68cf */
	.arch	armv8-a
	.file	"typemap.jm.inc"

	/* Mapping header */
	.section	.data.jm_typemap,"aw",@progbits
	.type	jm_typemap_header, @object
	.p2align	2
	.global	jm_typemap_header
jm_typemap_header:
	/* version */
	.word	1
	/* entry-count */
	.word	6230
	/* entry-length */
	.word	266
	/* value-offset */
	.word	121
	.size	jm_typemap_header, 16

	/* Mapping data */
	.type	jm_typemap, @object
	.global	jm_typemap
jm_typemap:
	.size	jm_typemap, 1657181
	.include	"typemap.jm.inc"
