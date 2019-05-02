﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers {
   public class AgentesController : Controller {

      // cria VAR que representa a BD
      private MultasDB db = new MultasDB();

      // GET: Agentes
      public ActionResult Index() {

         // procura a totalidade dos Agentes na BD

         // Instrução feita em LINQ
         // SELECT * FROM Agentes ORDER BY nome
         var listaAgentes = db.Agentes.OrderBy(a => a.Nome).ToList();

         return View(listaAgentes);
      }




      // GET: Agentes/Details/5
      /// <summary>
      /// Mostra os dados de um Agente 
      /// </summary>
      /// <param name="id">identifica o Agente</param>
      /// <returns>devolve a View com os dados</returns>
      public ActionResult Details(int? id) {

         if(id == null) {
            // vamos alterar esta resposta por defeito
            // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //
            // este erro ocorre porque o utilizador anda a fazer asneiras
            return RedirectToAction("Index");
         }

         // SELECT * FROM Agentes WHERE Id=id
         Agentes agente = db.Agentes.Find(id);

         // o Agente foi encontrado?
         if(agente == null) {
            // o Agente não foi encontrado, porque o utilizador está 'à pesca'
            // return HttpNotFound();
            return RedirectToAction("Index");
         }

         return View(agente);
      }




      // GET: Agentes/Create
      public ActionResult Create() {
         return View();
      }

      // POST: Agentes/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "ID,Nome,Esquadra,Fotografia")]
                               Agentes agente) {
         if(ModelState.IsValid) {
            db.Agentes.Add(agente);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(agente);
      }




      // GET: Agentes/Edit/5
      public ActionResult Edit(int? id) {

         if(id == null) {
            // vamos alterar esta resposta por defeito
            // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //
            // este erro ocorre porque o utilizador anda a fazer asneiras
            return RedirectToAction("Index");
         }

         // SELECT * FROM Agentes WHERE Id=id
         Agentes agente = db.Agentes.Find(id);

         // o Agente foi encontrado?
         if(agente == null) {
            // o Agente não foi encontrado, porque o utilizador está 'à pesca'
            // return HttpNotFound();
            return RedirectToAction("Index");
         }

         return View(agente);
      }

      // POST: Agentes/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agentes) {
         if(ModelState.IsValid) {
            db.Entry(agentes).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(agentes);
      }




      // GET: Agentes/Delete/5
      public ActionResult Delete(int? id) {

         if(id == null) {
            // vamos alterar esta resposta por defeito
            // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //
            // este erro ocorre porque o utilizador anda a fazer asneiras
            return RedirectToAction("Index");
         }

         // SELECT * FROM Agentes WHERE Id=id
         Agentes agente = db.Agentes.Find(id);

         // o Agente foi encontrado?
         if(agente == null) {
            // o Agente não foi encontrado, porque o utilizador está 'à pesca'
            // return HttpNotFound();
            return RedirectToAction("Index");
         }

         // o Agente foi encontrado
         // vou salvaguardar os dados para posterior validação
         // - guardar o ID do Agente num cookie cifrado
         // - guardar o ID numa variável de sessão 
         //       (se se usar o ASP .NET Core, esta ferramenta já não existe...)
         // - outras alternativas válidas...

         Session["Agente"] = agente.ID;

         // mostra na View os dados do Agente
         return View(agente);
      }




      // POST: Agentes/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int? id) {

         if(id == null) {
            // há um 'xico experto' a tentar dar-me a volta ao código
            return RedirectToAction("Index");
         }

         // o ID não é null
         // será o ID o que eu espero?
         // vamos validar se o ID está correto
         if(id!=(int)Session["Agente"]){
            // há aqui outro 'xico experto'...
            return RedirectToAction("Index");
         }



         // procura o Agente a remover
         Agentes agente = db.Agentes.Find(id);

         if(agente == null) {
            // não foi encontrado o Agente
            return RedirectToAction("Index");
         }


         try {
            // dá ordem de remoção do Agente
            db.Agentes.Remove(agente);

            // consolida a remoção
            db.SaveChanges();
         }
         catch(Exception) {
            // devem aqui ser escritas todas as instruções que são consideradas necessárias

            // informar q houve um erro
            ModelState.AddModelError("", "Não é possível remover o Agente " + agente.Nome + ". " +
            "Provavelmente, tem multas associadas a ele...");

            // redirecionar para a página onde o erro foi gerado
            return View(agente);
         }


         return RedirectToAction("Index");
      }




      protected override void Dispose(bool disposing) {
         if(disposing) {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
